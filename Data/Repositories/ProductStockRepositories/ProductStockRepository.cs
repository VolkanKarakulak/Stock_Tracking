using Data.Contexts;
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

namespace Data.Repositories.ProductStockRepositories
{
    public class ProductStockRepository : IProductStockRepository
    {
        private readonly IGenericRepository<ProductStock> _repository;
        private readonly Stock_TrackingDbContext _context;
        protected readonly DbSet<ProductStock> _dbSet;

        public ProductStockRepository(IGenericRepository<ProductStock> repository, Stock_TrackingDbContext context)
        {
            _repository = repository;
            _context = context;
            _dbSet = _context.Set<ProductStock>();

        }

        public async Task<bool> AnyAsync(Expression<Func<ProductStock, bool>> expression)
        {
            return await _repository.AnyAsync(expression);
        }

        public async Task<ProductStock?> CreateAsync(ProductStock entity)
        {
           
            return await _repository.CreateAsync(entity);
            
        }

        public async Task<ProductStock?> StateChangeAsync(ProductStock entity)
        {

            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return entity; 

        }

        public async Task<bool> IsEntityUpdateableAsync(int id)
        {
            return await _repository.IsEntityUpdateableAsync(id);
        }

        public async Task<ProductStock> UpdateAsync(ProductStock entity)
        {
            var entry = _context.Entry(entity);

            switch (entry.State)
            {
                case EntityState.Detached:
                    return await HandleDetachedEntity(entity);

                case EntityState.Modified:
                    entry.State = EntityState.Modified;
                    break;

                default:
                    throw new InvalidOperationException("Entity state is not valid for an update operation.");
            }

            await _context.SaveChangesAsync();
            return entity;
        }

        private async Task<ProductStock> HandleDetachedEntity(ProductStock entity)
        {
            var entityHelper = new EntityHelper<ProductStock>(_context);
            var oldEntity = entityHelper.GetOldEntity(entity.Id);

            if (oldEntity == null)
                throw new InvalidOperationException("Entity with the specified ID does not exist in the context.");

            var behavior = new UpdatedBehavior();
            behavior.ApplyBehavior(_context, entity);

            entityHelper.UpdateEntityProperties(oldEntity, entity);

            await _context.SaveChangesAsync();
            return oldEntity;
        }


        public async Task<bool> DeleteAsync(int id)  // Go to: Daha kısa ve esnek hale getirilebilir
		{
			// İlişkili product ve productStock'u getir
			var productStock = await _context.ProductStocks
				.Include(ps => ps.Product) // İlişkili Product'u dahil et
				.FirstOrDefaultAsync(ps => ps.Id == id);

			if (productStock == null) throw new KeyNotFoundException($"ProductStock with id {id} not found.");

				// ProductStock için soft delete
				productStock.IsActive = false;
				productStock.IsDeleted = true;

			// İlişkili Product için soft delete
			if (productStock.Product != null)
			{
				productStock.Product.IsActive = false;
				productStock.Product.IsDeleted = true;
			}

			// Değişiklikleri kaydet
			_context.ProductStocks.Update(productStock);
			await _context.SaveChangesAsync();

			return true;
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

        public Task<(int, int, IQueryable<ProductStock>)> GetPagedAsync(int pageNumber, int pageSize)
        {
            throw new NotImplementedException();
        }

		public async Task<ProductStock> GetByColumnAsync(Expression<Func<ProductStock, bool>> predicate)
		{
			return await _dbSet.FirstOrDefaultAsync(predicate)
				   ?? throw new Exception("Entity not found.");
		}

		public bool Delete(int id)
		{
			throw new NotImplementedException();
		}

		public Task<bool> DeleteWithProductAsync(int id)
		{
			throw new NotImplementedException();
		}
	}
}
