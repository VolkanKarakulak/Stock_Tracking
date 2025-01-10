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

namespace Data.Repositories.CategoryRepositories
{
    public class CategoryRepository : ICategoryRepository
    {

        private readonly IGenericRepository<Category> _repository;
        private readonly Stock_TrackingDbContext _context;
        protected readonly DbSet<Category> _dbSet;

        public CategoryRepository(IGenericRepository<Category> repository, Stock_TrackingDbContext context)
        {
            _repository = repository;
            _context = context;
            _dbSet = _context.Set<Category>();

        }

        public async Task<bool> AnyAsync(Expression<Func<Category, bool>> expression)
        {
            return await _repository.AnyAsync(expression);
        }

        public async Task<Category?> CreateAsync(Category entity)
        {
            return await _repository.CreateAsync(entity);
        }

        public async Task<IEnumerable<Category>> CreateRangeAsync(IEnumerable<Category> entities)
        {
           await _repository.CreateRangeAsync(entities);
           return entities;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await _repository.DeleteAsync(id);
        }

        public async Task<IQueryable<Category>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        //public bool DeleteRange(IEnumerable<int> entityIds)
        //{
        //   return _repository.DeleteRange(entityIds);
        //}

        //public async Task<IQueryable<Category>> GetAllAsync()
        //{
        //    return await _repository.GetAllAsync();
        //}

        public IQueryable<Category> GetBy(Expression<Func<Category, bool>> expression)
        {
            return _repository.GetBy(expression);
        }

        public async Task<Category> GetByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task<IEnumerable<Category>> GetByIdsAsync(IEnumerable<int> ids)
        {
            return await _context.Categories
                .AsNoTracking()
                .Where(category => ids.Contains(category.Id))
                .ToListAsync();
        }


        public async Task<(int, int, IQueryable<Category>)> GetPagedAsync(int pageNumber, int pageSize)
        {
            return await _repository.GetPagedAsync(pageNumber, pageSize);
        }

        public async Task<bool> IsEntityUpdateableAsync(int id)
        {
            return await _repository.IsEntityUpdateableAsync(id);
        }

        public async Task<Category> UpdateAsync(Category entity)
        {
           return await _repository.UpdateAsync(entity);
        }
    }
}
