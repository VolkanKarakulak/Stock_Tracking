using Data.Entities;
using Service.DTOs.OrderAdminDtos;
using Service.DTOs.OrderDtos;
using Service.Services.GenericService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services.OrderAdminService
{
	public interface IOrderAdminService : IGenericService<Order, OrderDto>
	{
		Task<OrderDto> UpdateOrderAsync(OrderUpdateByAdminDto dto);
	}
}
