using Data.Contexts;
using Data.Entities;
using Data.Repositories.GenericRepositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories.OrderRepositories
{
	public class OrderRepository : IOrderRepository
	{
		private readonly IGenericRepository<Order> _repository;
		private readonly Stock_TrackingDbContext _context;
		protected readonly DbSet<Order> _dbSet;

		public OrderRepository(IGenericRepository<Order> repository, Stock_TrackingDbContext appDbContext)
		{
			_repository = repository;
			_context = appDbContext;
			_dbSet = _context.Set<Order>();
		}

		public async Task<bool> AnyAsync(Expression<Func<Order, bool>> expression)
		{
			return await _repository.AnyAsync(expression);
		}

		public async Task<Order?> CreateAsync(Order entity)
		{
			await _repository.CreateAsync(entity);
			await _context.SaveChangesAsync();

			return entity;
		}

		//public Task<Order?> CreateAsync(Order entity)
		//{
		//	throw new NotImplementedException();
		//}

		public async Task<IEnumerable<Order>> CreateRangeAsync(IEnumerable<Order> entities)
		{
			return await _repository.CreateRangeAsync(entities);
		}

		public bool Delete(int id)
		{
			return _repository.Delete(id);
		}

		public async Task<IQueryable<Order>> GetAllAsync()
		{
			return await _repository.GetAllAsync();
		}

		public IQueryable<Order> GetBy(Expression<Func<Order, bool>> expression)
		{
			return _repository.GetBy(expression);
		}

		public async Task<Order> GetByIdAsync(int id)
		{
			return await _repository.GetByIdAsync(id);
		}

		public async Task<Order> GetOrderWithDetailsAsync(int orderId)
		{
			return await _dbSet
				.Include(o => o.OrderDetails)
				.ThenInclude(od => od.Product)
				.FirstOrDefaultAsync(o => o.Id == orderId);
		}

		public async Task<(int, int, IQueryable<Order>)> GetPagedAsync(int pageNumber, int pageSize)
		{
			return await _repository.GetPagedAsync(pageNumber, pageSize);
		}

		public async Task<bool> IsEntityUpdateableAsync(int id)
		{
			return await _repository.IsEntityUpdateableAsync(id);
		}

		public async Task<Order> UpdateAsync(Order entity)
		{
			return await _repository.UpdateAsync(entity);
		}
	}
}
