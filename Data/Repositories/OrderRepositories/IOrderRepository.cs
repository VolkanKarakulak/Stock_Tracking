using Data.Entities;
using Data.Repositories.GenericRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories.OrderRepositories
{
	public interface IOrderRepository : IGenericRepository<Order> 
	{
		//Task<Order?> CreateAsync(Order entity, List<OrderDetail> orderItems);
		Task<Order> GetOrderWithDetailsAsync(int orderId);
	}
}
