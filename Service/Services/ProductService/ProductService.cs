﻿using AutoMapper;
using AutoMapper.Internal.Mappers;
using Data.Entities;
using Data.Repositories.CategoryRepositories;
using Data.Repositories.GenericRepositories;
using Data.Repositories.ProductRepositories;
using Data.Repositories.ProductStockRepositories;
using Data.UnitOfWorks;
using Microsoft.EntityFrameworkCore;
using Service.DTOs.CategoryDtos;
using Service.DTOs.PaginationDto;
using Service.DTOs.ProductDtos;
using Service.DTOs.ProductStockDtos;
using Service.DTOs.ResponseDto;
using Service.Exceptions;
using Service.Exceptions.NotFoundExeptions;
using Service.Mapping;
using Service.Services.GenericService;


namespace Service.Services.ProductService
{
    public class ProductService : GenericService<Product>, IProductService
    {
        
        private readonly IProductRepository _productRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IProductStockRepository _productStockRepository;      
        private readonly IUnitOfWork _unitOfWork;


        public ProductService(IProductRepository productRepository, ICategoryRepository categoryRepository, IUnitOfWork unitOfWork, IProductStockRepository productStockRepository) : base(productRepository, unitOfWork)
        {
           
            _unitOfWork = unitOfWork;
            _categoryRepository = categoryRepository;
            _productStockRepository = productStockRepository;
            _productRepository = productRepository;
        }

        public override async Task<Product> CreateAsync(Product product)
        {
            var result = await _productRepository.CreateAsync(product);  

            if (result != null)
            {
                var productStock = ObjectMapper.Mapper.Map<ProductStock>(product);
                //productStock.ProductId = product.Id;
                await _productStockRepository.CreateAsync(productStock);
                return result;
            }
            else
            {
                throw new DataCreateFailedException();
            }
            
        }

        public override async Task<Product> UpdateAsync(Product product)
        {
            return await _productRepository.UpdateAsync(product);
        }

        public async Task<PagedResponseDto<IEnumerable<ProductDto>>> GetProductsByCategoryIdPagedAsync(int categoryId, PaginationDto paginationDto)
        {
            var categoryExist = await _categoryRepository
               .GetBy(x => x.Id == categoryId && !x.IsDeleted && x.IsActive)
               .FirstOrDefaultAsync() ?? throw new PageNotFoundException();

            var (totalPages, totalCount, courses) = await _productRepository.GetProductByCategoryIdPagedAsync(categoryId, paginationDto.PageNumber, paginationDto.PageSize);

            var productList = ObjectMapper.Mapper.Map<IEnumerable<ProductDto>>(courses);
            var categoryDto = ObjectMapper.Mapper.Map<ProductStockDto>(categoryExist);


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
