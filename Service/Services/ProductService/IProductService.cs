﻿using Data.Entities;
using Service.DTOs.PaginationDto;
using Service.DTOs.ProductDtos;
using Service.DTOs.ProductStockDtos;
using Service.DTOs.ResponseDto;
using Service.Services.GenericService;


namespace Service.Services.ProductService
{
    public interface IProductService : IGenericService<Product, ProductDto>
    {
        Task<ProductDto> CreateProductAsync(ProductAddDto entity);
        Task<ProductDto> UpdateProductAsync(ProductUpdateDto entity);
        Task<IEnumerable<ProductDto>> GetAllProductsAsync();
		Task<PagedResponseDto<IEnumerable<ProductDto>>> GetProductsByCategoryIdPagedAsync(int categoryId, PaginationDto paginationDto);
    }
}
