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

namespace Data.Repositories.UserRepositories
{
    public class UserRepository : IUserRepository
    {
        private readonly IGenericRepository<User> _repository;
        private readonly Stock_TrackingDbContext _context;
        protected readonly DbSet<User> _dbSet;

        public UserRepository(IGenericRepository<User> repository, Stock_TrackingDbContext context)
        {
            _repository = repository;
            _context = context;
            _dbSet = _context.Set<User>();
        }

        public async Task<User> GetByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task<IQueryable<User>> GetAllAsync()
        {
            var users = await _context.Users
                .AsNoTracking()
                .Include(p => p.UserRoles)
                .ToListAsync(); 

            return await Task.FromResult(users.AsQueryable());
        }

        public IQueryable<User> GetBy(Expression<Func<User, bool>> expression)
        {
            return _repository.GetBy(expression);
        }

        public async Task<bool> AnyAsync(Expression<Func<User, bool>> expression)
        {
            return await _repository.AnyAsync(expression);
        }

        public async Task<User?> CreateAsync(User entity)
        {
            return await _repository.CreateAsync(entity);
        }

        public Task<User> UpdateAsync(User entity)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> IsEntityUpdateableAsync(int id)
        {
           return await _repository.IsEntityUpdateableAsync(id);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await _repository.DeleteAsync(id);
        }

        public async Task<(int, int, IQueryable<User>)> GetPagedAsync(int pageNumber, int pageSize)
        {
            return await _repository.GetPagedAsync(pageNumber, pageSize);
        }

    
    }
}
