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
        //private readonly IGenericRepository<Product> _productRepository;

        public ProductStockRepository(Stock_TrackingDbContext context) : base(context)
        {
            
        }


        public override async Task<ProductStock?> CreateAsync(ProductStock entity)
        {
              
             _context.Entry(entity).State = EntityState.Modified;
             await _context.SaveChangesAsync();
             return entity; // Güncellenmiş stok kaydını döndür
           

        }

    }
}
