using Data.Entities;
using Data.Repositories.GenericRepositories;
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

        public CategoryRepository(IGenericRepository<Category> repository)
        {
            _repository = repository;
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

        public bool Delete(int id)
        {
            throw new NotImplementedException();
        }

        public bool DeleteRange(IEnumerable<Category> entities)
        {
            throw new NotImplementedException();
        }

        public Task<IQueryable<Category>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public IQueryable<Category> GetBy(Expression<Func<Category, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public Task<Category> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<(int, int, IQueryable<Category>)> GetPagedAsync(int pageNumber, int pageSize)
        {
            throw new NotImplementedException();
        }

        public Task<bool> IsEntityUpdateableAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Category Update(Category entity)
        {
            throw new NotImplementedException();
        }
    }
}
