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

namespace Data.Repositories.ProductRepositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly IGenericRepository<Product> _repository;
        private readonly Stock_TrackingDbContext _context;

        public ProductRepository(IGenericRepository<Product> repository, Stock_TrackingDbContext context)
        {
            _repository = repository;
            _context = context;

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

        public async Task<(int totalpage, int totalcount, IQueryable<Product>)> GetProductByCategoryIdPagedAsync(int categoryId, int pageNumber, int pageSize)
        {
            var categoryExists = await _context.ProductCategory

               .AsNoTracking()
               .AnyAsync(x => x.CategoryId == categoryId && x.Category.IsActive && !x.Category.IsDeleted);

            if (!categoryExists)
            {
                return (0, 0, Enumerable.Empty<Product>().AsQueryable());
            }

            var totalCount = await _context.ProductCategory
                .AsNoTracking()
                .Where(x => x.CategoryId == categoryId && x.Category.IsActive && !x.Category.IsDeleted)
                .Select(x => x.Product)
                .Where(c => c.IsActive && !c.IsDeleted)
                .CountAsync();

            int totalPages = (int)Math.Ceiling((double)totalCount / pageSize);

            if (pageNumber < 1 || pageNumber > totalPages)
            {
                return (0, 0, Enumerable.Empty<Product>().AsQueryable());
            }

            var pagedCourses = await _context.ProductCategory
                .AsNoTracking()
                .Where(x => x.CategoryId == categoryId && x.Category.IsActive && !x.Category.IsDeleted)
                .Include(x => x.Product)
                .Include(cc => cc.Category)
                .Select(x => x.Product)
                .Where(c => c.IsActive && !c.IsDeleted)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return (totalPages, totalCount, pagedCourses.AsQueryable());
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
