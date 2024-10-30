using Data.Contexts;
using Data.Entities;
using Data.Repositories.GenericRepositories;
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
        public async Task<bool> AnyAsync(Expression<Func<ProductStock, bool>> expression)
        {
            return await _repository.AnyAsync(expression); 
        }

        public async Task<ProductStock?> CreateAsync(ProductStock entity)
        {
           return await _repository.CreateAsync(entity);
        }

        public async Task<IEnumerable<ProductStock>> CreateRangeAsync(IEnumerable<ProductStock> entities)
        {
            await _repository.CreateRangeAsync(entities);
            return entities;
        }

        public bool Delete(int id)
        {
            return _repository.Delete(id);
        }

        public async Task<IQueryable<ProductStock>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        public IQueryable<ProductStock> GetBy(Expression<Func<ProductStock, bool>> expression)
        {
            return _repository.GetBy(expression);
        }

        public async Task<ProductStock> GetByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task<(int, int, IQueryable<ProductStock>)> GetPagedAsync(int pageNumber, int pageSize)
        {
            return await _repository.GetPagedAsync(pageNumber, pageSize);
        }

        public async Task<bool> IsEntityUpdateableAsync(int id)
        {
            return await _repository.IsEntityUpdateableAsync(id);
        }

        public ProductStock Update(ProductStock entity)
        {
            return _repository.Update(entity);
        }
    }
}
