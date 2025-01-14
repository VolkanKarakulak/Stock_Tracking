﻿using Data.Entities;
using Service.DTOs.OrderDtos;
using Service.Services.GenericService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services.OrderService
{
	public interface IOrderService : IGenericService<Order, OrderDto>
	{
		Task<OrderDto> CreateOrderAsync (OrderAddDto dto);
	}
}
