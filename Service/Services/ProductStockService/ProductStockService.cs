using AutoMapper;
using AutoMapper.Internal.Mappers;
using Data.Entities;
using Data.Repositories.GenericRepositories;
using Data.Repositories.ProductRepositories;
using Data.Repositories.ProductStockRepositories;
using Data.UnitOfWorks;
using Microsoft.EntityFrameworkCore;
using Service.DTOs.ProductStockDtos;
using Service.Exceptions.NotFoundExeptions;
using Service.Mapping;
using Service.Services.GenericService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services.ProductStockService
{
    public class ProductStockService : GenericService<ProductStock, ProductStockDto>, IProductStockService
    {
        private readonly IProductRepository _productRepository;
        private readonly IProductStockRepository _productStockRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public ProductStockService(IProductRepository productRepository, IUnitOfWork unitOfWork, IProductStockRepository productStockRepository, IMapper mapper) : base(productStockRepository, unitOfWork, mapper)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _productRepository = productRepository;
            _productStockRepository = productStockRepository;
                 
        }
        public async Task<ProductStockDto> CreateProductStockAsync(ProductStockAddDto entity)
        {
            var existingStock = await _productStockRepository.GetBy(p => p.ProductId == entity.ProductId).FirstOrDefaultAsync();

            var product = await _productRepository.GetByIdAsync(entity.ProductId);

            if (product == null)
            {
                throw new DataNotFoundException();
            }
            if (existingStock != null)
            {
                existingStock.Quantity += entity.Quantity;
                product.Stock = existingStock.Quantity;
                await _productStockRepository.StateChangeAsync(existingStock);
                await _productRepository.UpdateAsync(product);

                return _mapper.Map<ProductStockDto>(existingStock);
            }
            else
            {                
                product.Stock += entity.Quantity;
                await _productRepository.UpdateAsync(product);
                entity.Quantity = product.Stock;
                //var addProductStock = _mapper.Map<ProductStock>(product);
                //await _productStockRepository.CreateAsync(addProductStock);
                var productStock = _mapper.Map<ProductStock>(entity);
                var result = await _productStockRepository.CreateAsync(productStock);
                return _mapper.Map<ProductStockDto>(result);
            }
        }


        //public override async Task<ProductStock> UpdateAsync(ProductStock entity)
        //{
        //    // Ürünü ve stok kaydını al
        //    var product = await _productRepository.GetBy(p => p.Id == entity.ProductId)
        //        .AsNoTracking()
        //        .FirstOrDefaultAsync();

        //    var productStock = await _productStockRepository.GetBy(p => p.ProductId == entity.ProductId)
        //        .AsNoTracking()
        //        .FirstOrDefaultAsync();

        //    // Ürün bulunamazsa hata fırlat
        //    if (product == null)
        //    {
        //        throw new DataNotFoundException();
        //    }
                
        //    // ProductStockUpdateDto'ya haritalama yap
        //    var productStockDto = ObjectMapper.Mapper.Map<ProductStockUpdateDto>(entity);

        //    if (productStock != null)
        //    {
        //        // Mevcut productStock ve product üzerinde güncellemeleri yap
        //        ObjectMapper.Mapper.Map(productStockDto, productStock);
        //        ObjectMapper.Mapper.Map(productStockDto, product);

        //        await _productStockRepository.UpdateAsync(productStock);
        //    }
        //    else
        //    {
        //        // Yeni bir stok kaydı oluştur
        //        productStock = ObjectMapper.Mapper.Map<ProductStock>(productStockDto);
        //        await _productStockRepository.CreateAsync(productStock);
        //        ObjectMapper.Mapper.Map(productStockDto, product);
        //    }

        //    // Ürünü güncelle
        //    await _productRepository.UpdateAsync(product);

        //    return productStock;
        //}

    }
}
