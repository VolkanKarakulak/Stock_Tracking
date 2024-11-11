﻿using AutoMapper;
using Data.Entities;
using Data.Repositories.CategoryRepositories;
using Data.Repositories.ProductRepositories;
using Data.Repositories.ProductStockRepositories;
using Data.UnitOfWorks;
using Microsoft.EntityFrameworkCore;
using Service.DTOs.PaginationDto;
using Service.DTOs.ProductDtos;
using Service.DTOs.ProductStockDtos;
using Service.DTOs.ResponseDto;
using Service.Exceptions;
using Service.Exceptions.NotFoundExeptions;
using Service.Services.GenericService;


namespace Service.Services.ProductService
{
    public class ProductService : GenericService<Product, ProductDto>, IProductService
    {
        
        private readonly IProductRepository _productRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IProductStockRepository _productStockRepository;      
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;


        public ProductService(IProductRepository productRepository, ICategoryRepository categoryRepository, IUnitOfWork unitOfWork, IProductStockRepository productStockRepository, IMapper mapper) : base(productRepository, unitOfWork, mapper)
        {

            _unitOfWork = unitOfWork;
            _categoryRepository = categoryRepository;
            _productStockRepository = productStockRepository;
            _productRepository = productRepository;
            _mapper = mapper;
        }

        public async Task<ProductDto> CreateProductAsync(ProductAddDto entity)
        {
            var product = _mapper.Map<Product>(entity);
            var productCreateResult = await _productRepository.CreateAsync(product);
            await _unitOfWork.CommitAsync();
            if (productCreateResult != null)
            {
                var productStock = _mapper.Map<ProductStock>(entity);
                productStock.ProductId = product.Id;
                await _productStockRepository.CreateAsync(productStock);
                await _unitOfWork.CommitAsync();
                var result = _mapper.Map<ProductDto>(productCreateResult);
                return result;
            }
            else
            {
                throw new DataCreateFailedException();
            }

        }

        public async Task<ProductDto> UpdateProductAsync(ProductUpdateDto entity)
        {
            var isUpdateableProduct = await _productRepository.IsEntityUpdateableAsync(entity.Id);

            var product = await _productRepository.GetBy(p => p.Id == entity.Id)
                .AsNoTracking()
                .FirstOrDefaultAsync();

            if (product == null)
            {
                throw new DataNotFoundException();
            }

            var productStock = await _productStockRepository.GetBy(p => p.Id == entity.ProductStockId)
                .AsNoTracking()
                .FirstOrDefaultAsync();


            if (productStock != null)
            {
                _mapper.Map(entity, productStock);
                _mapper.Map(entity, product);

                await _productStockRepository.UpdateAsync(productStock);
                await _unitOfWork.CommitAsync();
            }
            else
            {
                productStock = _mapper.Map<ProductStock>(entity);
                await _productStockRepository.CreateAsync(productStock);
                await _unitOfWork.CommitAsync();
                //_mapper.Map(entity, product);
            }

            //if (isUpdateableProduct)
            //{
            //    var product = _mapper.Map<Product>(entity);
            //    await _productRepository.UpdateAsync(product);            

            //    var productStock = _mapper.Map<ProductStock>(entity);
            //    await _productStockRepository.UpdateAsync(productStock);
            //    await _unitOfWork.CommitAsync();

            //    var productDto = _mapper.Map<ProductDto>(product);
            //    return productDto;

            //}

            await _productRepository.UpdateAsync(product);
            await _unitOfWork.CommitAsync();
            var productDto = _mapper.Map<ProductDto>(product);
            return productDto;

        }

        public async Task<PagedResponseDto<IEnumerable<ProductDto>>> GetProductsByCategoryIdPagedAsync(int categoryId, PaginationDto paginationDto)
        {
            var categoryExist = await _categoryRepository
               .GetBy(x => x.Id == categoryId && !x.IsDeleted && x.IsActive)
               .FirstOrDefaultAsync() ?? throw new PageNotFoundException();

            var (totalPages, totalCount, courses) = await _productRepository.GetProductByCategoryIdPagedAsync(categoryId, paginationDto.PageNumber, paginationDto.PageSize);

            var productList = _mapper.Map<IEnumerable<ProductDto>>(courses);
            var categoryDto = _mapper.Map<ProductStockDto>(categoryExist);


            var pagedResponseDto = GeneratePagedResponseModel(
                    categoryId,
                    paginationDto,
                    totalPages,
                    totalCount,
                    productList // Sadece ürün listesini döndürüyoruz
                );

            return pagedResponseDto;
        }

        private PagedResponseDto<TModel> GeneratePagedResponseModel<TModel>(
            int id,
            PaginationDto model,
            int totalPages,
            int totalCount,
            TModel pagedDto)
            where TModel : class
        {
            return new PagedResponseDto<TModel>
            {
                PagedDto = pagedDto,
                TotalPages = totalPages,
                TotalCount = totalCount,
                Id = id,
                PageNumber = model.PageNumber
            };
        }

    }

}
