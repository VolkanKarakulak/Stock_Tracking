﻿using AutoMapper;
using Data.Entities;
using Data.Repositories.CategoryRepositories;
using Data.Repositories.ProductRepositories;
using Data.Repositories.ProductStockRepositories;
using Data.Repositories.TaxSettingRepositories;
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
        private readonly ITaxSettingRepository _taxSettingRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

		public ProductService(IProductRepository productRepository, ICategoryRepository categoryRepository, IUnitOfWork unitOfWork, IProductStockRepository productStockRepository, IMapper mapper, ITaxSettingRepository taxSettingRepository) : base(productRepository, unitOfWork, mapper)
		{

			_unitOfWork = unitOfWork;
			_categoryRepository = categoryRepository;
			_productStockRepository = productStockRepository;
			_productRepository = productRepository;
			_mapper = mapper;
			_taxSettingRepository = taxSettingRepository;
		}


		public async Task<ProductDto> CreateProductAsync(ProductAddDto dto)
        {
            var taxRate = await _taxSettingRepository.GetTaxRateAsync();
            var productPrice = dto.Price * (1 + taxRate);

            var product = _mapper.Map<Product>(dto);
            if (product == null)
            {
                throw new ArgumentNullException(nameof(product), "Product nesnesi null olamaz.");
            }

			// Fiyatı güncelle
			product.Price = productPrice;

			var categories = await _categoryRepository.GetByIdsAsync(dto.CategoryIds);
            if (categories == null)
            {
                throw new ArgumentNullException(nameof(categories), "Categories koleksiyonu null olamaz.");
            }
            // Eğer ProductCategories null ise, yeni bir liste başlatıyoruz
            product.ProductCategories ??= new List<ProductCategory>();

            product.ProductCategories = categories.Select(category => new ProductCategory
            {
                CategoryId = category.Id
            }).ToList();


            var productCreateResult = await _productRepository.CreateAsync(product);
            await _unitOfWork.CommitAsync();

            if (productCreateResult != null)
            {
                var productStock = _mapper.Map<ProductStock>(product);
                if (productStock == null)
                {
                    throw new Exception("Mapping işlemi sırasında productStock nesnesi oluşturulamadı.");
                }
                productStock.ProductId = product.Id;
                var createdProductStock = await _productStockRepository.CreateAsync(productStock);
                await _unitOfWork.CommitAsync();
                           
                var result = _mapper.Map<ProductDto>(productCreateResult);
                return result;
            }
            else
            {
                throw new DataCreateFailedException();
            }
        }

        public async Task<ProductDto> UpdateProductAsync(ProductUpdateDto dto)
        {
            var isUpdateableProduct = await _productRepository.IsEntityUpdateableAsync(dto.Id);

            var product = await _productRepository.GetBy(p => p.Id == dto.Id)
               .AsNoTracking()
               .FirstOrDefaultAsync();

            if (isUpdateableProduct || product != null)
            {              
                _mapper.Map(dto, product);

                await _productRepository.UpdateAsync(product);
                await _unitOfWork.CommitAsync();

                var productStock = await _productStockRepository
                    .GetBy(p => p.ProductId == product.Id)
                    .AsNoTracking()
                    .FirstOrDefaultAsync();

                if (productStock != null)
                {
                    _mapper.Map(product, productStock);
                    await _productStockRepository.UpdateAsync(productStock);
                    await _unitOfWork.CommitAsync();
                }
                else
                {
                    productStock = _mapper.Map<ProductStock>(dto);
                    await _productStockRepository.CreateAsync(productStock);
                    var productStockResult = await _productStockRepository
                        .GetBy(p => p.ProductId == productStock.ProductId)
                        .AsNoTracking()
                        .FirstOrDefaultAsync();

                    _mapper.Map(productStockResult, product);
                    //await _unitOfWork.CommitAsync();
                }
            }
            else
            {
                throw new DataNotFoundException();
            }
           
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

		public async Task<IEnumerable<ProductDto>> GetAllProductsAsync()
		{
			var products = await _productRepository.GetAllAsync();
			return _mapper.Map<IEnumerable<ProductDto>>(products);
		}
	}

}
