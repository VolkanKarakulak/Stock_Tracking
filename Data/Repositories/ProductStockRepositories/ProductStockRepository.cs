using Data.Contexts;
using Data.Entities;
using Data.Repositories.GenericRepositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories.ProductStockRepositories
{
    public class ProductStockRepository : GenericRepository<ProductStock>, IProductStockRepository
    {
        private readonly IGenericRepository<Product> _productRepository;

        public ProductStockRepository(Stock_TrackingDbContext context, IGenericRepository<Product> productRepository) : base(context)
        {
            _productRepository = productRepository;
        }


        public override async Task<ProductStock?> CreateAsync(ProductStock entity)
        {
            // Öncelikle belirtilen ürün ID'sine sahip mevcut stok kaydını alıyoruz
            var existingStock = await _dbSet.FirstOrDefaultAsync(ps => ps.ProductId == entity.ProductId);

            // Ürünü al
            var product = await _context.Products.FirstOrDefaultAsync(p => p.Id == entity.ProductId);

            if (existingStock != null)
            {
                // Eğer stok zaten varsa, mevcut miktara yeni miktarı ekleyin
                existingStock.Quantity += entity.Quantity;

                // Ürün stok miktarını güncelle
                if (product != null)
                {
                    product.Stock = existingStock.Quantity; // Güncellenmiş stok miktarını ata
                    _context.Entry(product).State = EntityState.Modified; // Ürün kaydını güncelle
                }

                // Stok kaydını güncelleyin
                _context.Entry(existingStock).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return existingStock; // Güncellenmiş stok kaydını döndür
            }
            else
            {
                // Eğer stok kaydı yoksa, yeni bir stok ekleyin
                entity.IsActive = true;
                await _dbSet.AddAsync(entity);

                // Ürün stok miktarını güncelle
                if (product != null)
                {
                    product.Stock = entity.Quantity; // Yeni stok miktarını ata
                    _context.Entry(product).State = EntityState.Modified; // Ürün kaydını 
                }
                await _context.SaveChangesAsync();
                return entity; // Yeni eklenmiş stok kaydını döndür
            }

        }

    }
}
