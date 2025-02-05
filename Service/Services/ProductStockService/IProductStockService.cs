﻿using Data.Entities;
using Service.DTOs.ProductStockDtos;
using Service.Services.GenericService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services.ProductStockService
{
    public interface IProductStockService : IGenericService<ProductStock, ProductStockDto>
    {
        Task<ProductStockDto> CreateProductStockAsync(ProductStockAddDto entity);
        Task<ProductStockDto> UpdateProductStockAsync(ProductStockUpdateDto entity);
    }
}
