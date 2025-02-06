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
        private const decimal CurrencyConversionRate = 100m; // Kuruştan TL'ye çevirme oranı

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

		
		public async Task<bool> DeleteAsync(int id)
		{
			return await _repository.DeleteAsync(id);
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
			var order = await _dbSet
				.Include(o => o.OrderDetails!)
				.ThenInclude(od => od.Product)
				.FirstOrDefaultAsync(o => o.Id == orderId);

			if (order == null)
			{
				throw new InvalidOperationException($"Order with ID {orderId} not found.");
			}

			return order; // Null kontrolü sonrası güvenli şekilde döndürülür.
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

        public async Task<int> GetPendingOrdersCountAsync()
        {
			var pendingOrders = await _context.Orders
				.Where(order => order.Status == Enums.OrderStatus.Pending )
				.CountAsync();

			return pendingOrders;
        }

        public async Task<int> GetTodayOrdersCountAsync()
        {
			var today  = DateTime.Today;
            var tomorrow = today.AddDays(1);

            var todayOrders = await _context.Orders
                .Where(order => order.CreatedDate >= today && order.CreatedDate < tomorrow)
                .CountAsync();

            return todayOrders;
        }

        private decimal ConvertToCurrency(decimal amount)
        {
            return amount / CurrencyConversionRate;
        }

        public async Task<decimal> GetDailyEarningsAsync()
        {
            var today = DateTime.Today;
            var tomorrow = today.AddDays(1);

            var dailyEarnings = await _context.Orders
                .Where(order => order.PaymentDate >= today && order.PaymentDate < tomorrow && order.IsPaid == true && order.IsCancelled == false)
                .SumAsync(order => order.TotalAmount);

            return ConvertToCurrency(dailyEarnings);
        }

        public async  Task<int> GetTotalOrdersAsync()
        {
            var orderCount = await _context.Orders.CountAsync(order => !order.IsCancelled);
            return orderCount;
        }

        public async Task<IQueryable<Order>> GetLastTenOrdersAsync()
        {
			var lastOrders = await _context.Orders
				.AsNoTracking()
				.Include(o => o.Customer)
                .OrderByDescending(o => o.CreatedDate)
                .Take(10).ToListAsync();

            return await Task.FromResult(lastOrders.AsQueryable());
        }
    }
}
