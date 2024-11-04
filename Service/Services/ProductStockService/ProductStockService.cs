using Data.Entities;
using Data.Repositories.GenericRepositories;
using Data.Repositories.ProductRepositories;
using Data.Repositories.ProductStockRepositories;
using Data.UnitOfWorks;
using Microsoft.EntityFrameworkCore;
using Service.DTOs.ProductStockDtos;
using Service.Exceptions.NotFoundExeptions;
using Service.Services.GenericService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services.ProductStockService
{
    public class ProductStockService : GenericService<ProductStock>, IProductStockService
    {
        private readonly IProductRepository _productRepository;
        private readonly IGenericRepository<ProductStock> _genericRepository;
        private readonly IProductStockRepository _productStockRepository;
        private readonly IUnitOfWork _unitOfWork;
        public ProductStockService(IProductRepository productRepository, IGenericRepository<ProductStock> genericRepository, IUnitOfWork unitOfWork, IProductStockRepository productStockRepository) : base(genericRepository, unitOfWork)
        {
            _productRepository = productRepository;
            _genericRepository = genericRepository;
            _unitOfWork = unitOfWork;
            _productStockRepository = productStockRepository;
        }
        public override async Task<ProductStock> CreateAsync(ProductStock entity) 
        {
     
            var existingStock = await _genericRepository.GetBy(p => p.ProductId == entity.ProductId).FirstOrDefaultAsync();

            // Ürün stok güncelleme işlemi
            var product = await _productRepository.GetByIdAsync(entity.ProductId);

            if (product == null)
            {
                throw new DataNotFoundException();
            }

            if (existingStock != null)
            {
                existingStock.Quantity += entity.Quantity;
                product.Stock = existingStock.Quantity;               
                await _productStockRepository.CreateAsync(existingStock);
                _productRepository.Update(product);
                
                return existingStock;
            }
            
            else
            {
               
                await _genericRepository.CreateAsync(entity);
                product.Stock = entity.Quantity;
                _productRepository.Update(product);
            }
          
            
            
            return entity; // Güncellenmiş veya yeni eklenmiş stok kaydını döndür
        }
    }
}
