﻿using Data.Contexts;
using Data.Entities;
using Data.EntityHelper;
using Data.Interceptors;
using Data.Repositories.GenericRepositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using static Data.Interceptors.ModifiedBehavior;

namespace Data.Repositories.ProductRepositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly IGenericRepository<Product> _repository;
        private readonly Stock_TrackingDbContext _context;
        protected readonly DbSet<Product> _dbSet;

        public ProductRepository(IGenericRepository<Product> repository, Stock_TrackingDbContext context)
        {
            _repository = repository;
            _context = context;
            _dbSet = _context.Set<Product>();
        }

        public async Task<bool> AnyAsync(Expression<Func<Product, bool>> expression)
        {
            return await _repository.AnyAsync(expression);
        }

        public async Task<Product?> CreateAsync(Product entity)
        {
           return await _repository.CreateAsync(entity);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await _repository.DeleteAsync(id);
        }

        //public bool DeleteRange(IEnumerable<int> entityIds)
        //{
            
        //    return _repository.DeleteRange(entityIds);
 
        //}

        public async Task<IQueryable<Product>> GetAllAsync()
        {
			var products = await _context.Products
                .AsNoTracking()
		        .Include(p => p.ProductCategories!)
                .ThenInclude(p => p.Category)// Product ile ilişkili CategoryIds
		        .Include(p => p.ProductStock)
				.Include(p => p.ProductSuppliers)
				.ToListAsync(); // Product ile ilişkili ProductStockId

			return await Task.FromResult(products.AsQueryable()); // IQueryable olarak döner
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
            var categoryExists = await _context.ProductCategories

               .AsNoTracking()
               .AnyAsync(x => x.CategoryId == categoryId && x.Category.IsActive && !x.Category.IsDeleted);

            if (!categoryExists)
            {
                return (0, 0, Enumerable.Empty<Product>().AsQueryable());
            }

            var totalCount = await _context.ProductCategories
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

            var pagedCourses = await _context.ProductCategories
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

		public async Task<Product> UpdateAsync(Product entity)
		{
			// Entity'nin durumu kontrol ediliyor
			var entry = _context.Entry(entity);

			if (entry.State == EntityState.Detached)
			{
				// Entity "detached" durumda ise, eski varlık bulunuyor
				var entityHelper = new EntityHelper<Product>(_context);
				var oldEntity = entityHelper.GetOldEntity(entity.Id);

				if (oldEntity != null)
				{
					// Değişiklik davranışı uygulanıyor
					var behavior = new ModifiedBehavior();
					behavior.ApplyBehavior(_context, entity);

					// Eski varlık özellikleri güncelleniyor
					entityHelper.UpdateEntityProperties(oldEntity, entity);

					return oldEntity;
				}
				else
				{
					throw new InvalidOperationException("Entity with the specified ID does not exist in the context.");
				}
			}
			else if (entry.State == EntityState.Modified)
			{
				// Eğer varlık "Modified" durumundaysa, doğrudan güncelleniyor
				entry.State = EntityState.Modified;
			}
			else
			{
				throw new InvalidOperationException("Entity state is not valid for an update operation.");
			}

			// Değişiklikler kaydediliyor
			await _context.SaveChangesAsync();
			return entity; // Güncellenmiş varlık döndürülüyor
		}


	}
}
