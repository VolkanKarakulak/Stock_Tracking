﻿using Data.Contexts;
using Data.Entities;
using Data.EntityHelper;
using Data.Interceptors;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq.Expressions;



namespace Data.Repositories.GenericRepositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        protected readonly Stock_TrackingDbContext _context;
        protected readonly DbSet<T> _dbSet;

        public GenericRepository(Stock_TrackingDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        public async Task<bool> AnyAsync(Expression<Func<T, bool>> expression)
        {
            return await _dbSet.AnyAsync(expression);
        }

        public async Task<T?> CreateAsync(T entity)
        {
            var behavior = new AddedBehavior();
            behavior.ApplyBehavior(_context, entity);
            entity.IsActive = true;

            // Entity'yi ekle
            await _dbSet.AddAsync(entity);
            //await _context.SaveChangesAsync();

            // `SaveChangesAsync` sonrasında durum `Unchanged` olarak görünür
            return _context.Entry(entity).State == EntityState.Added ? entity : null;
        }

        public async Task<bool> IsEntityUpdateableAsync(int id)
        {
            return await _dbSet.AnyAsync(x => x.Id == id && !x.IsDeleted);
        }
		public virtual async Task<T> UpdateAsync(T entity)
		{
			var entry = _context.Entry(entity);

			// Eğer varlık 'Detached' durumundaysa
			if (entry.State == EntityState.Detached)
			{
				var entityHelper = new EntityHelper<T>(_context);

				// Eski varlığı bul
				var oldEntity = entityHelper.GetOldEntity(entity.Id);
				if (oldEntity != null)
				{
					var behavior = new ModifiedBehavior();
					behavior.ApplyBehavior(_context, entity);

					// Eski varlıktaki 'IsDeleted' özelliğini koruyoruz
					entry.Property("IsDeleted").CurrentValue = oldEntity.IsDeleted;

					// Eski varlığı güncelle
					entityHelper.UpdateEntityProperties(oldEntity, entity);

					return oldEntity; // Güncellenmiş eski varlığı döndür
				}
				else
				{
					throw new InvalidOperationException("Entity with the specified ID does not exist in the context.");
				}
			}
			else
			{
				// Eğer varlık 'Attached' durumundaysa, durumunu güncelle
				entry.State = EntityState.Modified;
			}

			// Değişiklikleri kaydet
			await _context.SaveChangesAsync();

			return entity; // Güncellenmiş varlığı döndür
		}



		public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _dbSet.FindAsync(id);
            if (entity != null && !entity.IsDeleted)
            {
                entity.UpdatedDate = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd HH:mm"));
                entity.IsDeleted = true;
                entity.IsActive = false;
                return true;
            }
            return false;

        }

        //public bool DeleteRange(IEnumerable<int> entityIds)
        //{

        //    foreach (var Id in entityIds)
        //    {
        //        var propertyInfo = Id.GetType().GetProperty("IsActive");

        //        if(propertyInfo != null)
        //        {
        //            propertyInfo.SetValue(Id, false);
        //        }
        //    }
        //    return _context.SaveChanges() > 0;

        //}

        public async Task<IQueryable<T>> GetAllAsync()
        {
            return await Task.FromResult(_dbSet.AsNoTracking().AsQueryable());
        }

        public IQueryable<T> GetBy(Expression<Func<T, bool>> expression)
        {
            return _dbSet.Where(expression);
        }

        public async Task<T> GetByIdAsync(int id)
        {
            var entity = await _dbSet.FindAsync(id);

            if (entity == null)
            {
                throw new Exception("Entity with Id " + id + " not found.");
            }
            return entity;
        }

        public async Task<(int, int, IQueryable<T>)> GetPagedAsync(int pageNumber, int pageSize)
        {
           int totalCount = await _dbSet.Where(x => !x.IsDeleted).CountAsync();
           int totalPages = (int)Math.Ceiling((double)totalCount/pageSize);

            if(pageNumber < 1 || pageNumber > totalPages) 
            { 
                return(0, 0, Enumerable.Empty<T>().AsQueryable());
            }

            var paged = await _dbSet
                .Where(x => !x.IsDeleted)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .AsNoTracking()
                .ToListAsync();
            return (totalPages, totalCount, paged.AsQueryable());
        }
        
    }
}
