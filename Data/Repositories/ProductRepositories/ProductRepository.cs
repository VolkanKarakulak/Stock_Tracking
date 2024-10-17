using Data.Entities;
using Data.Repositories.GenericRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories.ProductRepositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly IGenericRepository<Product> _repository;

        public ProductRepository(IGenericRepository<Product> repository)
        {
            _repository = repository;
        }

        public async Task<bool> AnyAsync(Expression<Func<Product, bool>> expression)
        {
            return await _repository.AnyAsync(expression);
        }

        public async Task<Product?> CreateAsync(Product entity)
        {
            return await _repository.CreateAsync(entity);
        }

        public async Task CreateRangeAsync(IEnumerable<Product> entities)
        {
            await _repository.CreateRangeAsync(entities);
        }

        public bool Delete(int id)
        {
            return _repository.Delete(id);
        }

        public bool DeleteRange(IEnumerable<Product> entities)
        {
            return _repository.DeleteRange(entities);
        }

        public async Task<IQueryable<Product>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        public IQueryable<Product> GetBy(Expression<Func<Product, bool>> expression)
        {
           return _repository.GetBy(expression);
        }

        public async Task<Product> GetByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task<(int, int, IQueryable<Product>)> GetPagedAsync(int pageNumber, int pageSize)
        {
            return await _repository.GetPagedAsync(pageNumber, pageSize);
        }

        public async Task<bool> IsEntityUpdateableAsync(int id)
        {
            return await _repository.IsEntityUpdateableAsync(id);
        }

        public Product Update(Product entity)
        {
            return _repository.Update(entity);
        }
    }
}
