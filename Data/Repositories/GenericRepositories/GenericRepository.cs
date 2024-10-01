using Data.Contexts;
using Data.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;


namespace Data.Repositories.GenericRepositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        protected readonly Stock_TrackingDbContext _context;
        private readonly DbSet<T> _dbSet;

        public GenericRepository(Stock_TrackingDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        public async Task<bool> AnyAsync(Expression<Func<T, bool>> expression)
        {
            return await _dbSet.AnyAsync(expression);
        }

        public async Task CreateAsync(T entity)
        {
            await _dbSet.AddAsync(entity);

        }

        public async Task CreateRangeAsync(IEnumerable<T> entities)
        {

            await _context.AddRangeAsync(entities);

        }

        public void Delete(T entity)
        {
            entity.IsDeleted = true;

        }

        public void DeleteRange(IEnumerable<T> entities)
        {
            _dbSet.RemoveRange(entities);

        }

        public IQueryable<T> GetAll()
        {
            return _dbSet.AsNoTracking().AsQueryable();
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

        public void Update(T entity)
        {
            _dbSet.Update(entity);

        }

        public IQueryable<T> Where(Expression<Func<T, bool>> expression)
        {
            return _dbSet.Where(expression);
        }
    }
}
