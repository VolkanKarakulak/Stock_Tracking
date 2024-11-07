using Data.Contexts;
using Data.Entities;
using Data.Interceptors;
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
    public class ProductStockRepository : IProductStockRepository
    {
        private readonly IGenericRepository<ProductStock> _repository;
        private readonly Stock_TrackingDbContext _context;

        public ProductStockRepository(IGenericRepository<ProductStock> repository, Stock_TrackingDbContext context)
        {
            _repository = repository;
            _context = context;

        }

        public Task<bool> AnyAsync(Expression<Func<ProductStock, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public async Task<ProductStock?> CreateAsync(ProductStock entity)
        {
           
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return entity; // Güncellenmiş stok kaydını döndür
            
        }

        public async Task<bool> IsEntityUpdateableAsync(int id)
        {
            return await _repository.IsEntityUpdateableAsync(id);
        }

        public async Task<ProductStock> UpdateAsync(ProductStock entity)
        {
             _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return entity;
        }

        public Task<IEnumerable<ProductStock>> CreateRangeAsync(IEnumerable<ProductStock> entities)
        {
            throw new NotImplementedException();
        }

        public bool Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IQueryable<ProductStock>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public IQueryable<ProductStock> GetBy(Expression<Func<ProductStock, bool>> expression)
        {
            return _repository.GetBy(expression);
        }


        public Task<ProductStock> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<(int, int, IQueryable<ProductStock>)> GetPagedAsync(int pageNumber, int pageSize)
        {
            throw new NotImplementedException();
        }

        
    }
}
