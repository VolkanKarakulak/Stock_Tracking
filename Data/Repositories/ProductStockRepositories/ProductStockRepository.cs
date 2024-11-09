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

        public Task<bool> AnyAsync(Expression<Func<ProductStock, bool>> expression)
        {
            throw new NotImplementedException();
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
            {
                var entry = _context.Entry(entity);

                // Eğer entity 'Detached' durumda ise, eski varlığı bul.
                if (entry.State == EntityState.Detached)
                {
                    var entityHelper = new EntityHelper<ProductStock>(_context);

                    var oldEntity = entityHelper.GetOldEntity(entity.Id);
                    if (oldEntity != null)
                    {
                        var behavior = new UpdatedBehavior();
                        behavior.ApplyBehavior(_context, entity);

                        entityHelper.UpdateEntityProperties(oldEntity, entity);
                        await _context.SaveChangesAsync();
                        return oldEntity;
                    }
                }
                else
                {
                    // Mevcut varlığı güncelle.
                    _dbSet.Add(entity);
                }

                await _context.SaveChangesAsync(); // kalkabilir 
                return entity; // Eğer entity 'Attached' durumunda ise, kendi başına güncellenmiş varlığı döndür.
            }
        
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
