﻿using AutoMapper;
using Data.Entities;
using Data.Repositories.GenericRepositories;
using Data.Repositories.OrderRepositories;
using Data.UnitOfWorks;
using Service.DTOs.OrderDtos;
using Service.DTOs.PaginationDto;
using Service.DTOs.ResponseDto;
using Service.Helper;
using Service.Services.GenericService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services.OrderService
{
	public class OrderService : GenericService<Order, OrderDto>, IOrderService
	{
		private readonly IMapper _mapper;
		private readonly IOrderRepository _orderRepository;
		private readonly IUnitOfWork _unitOfWork;
		public OrderService(IGenericRepository<Order> repository, IUnitOfWork unitOfWork, IMapper mapper, IOrderRepository orderRepository) : base(repository, unitOfWork, mapper)
		{
			_mapper = mapper;
			_orderRepository = orderRepository;
			_unitOfWork = unitOfWork;
		}

		public async Task<OrderDto> CreateOrderAsync(OrderAddDto dto)
		{
			var order = _mapper.Map<Order>(dto);

			if (order == null)
			{
				throw new InvalidOperationException();
			}
			// Sipariş numarasını oluşturuyoruz (örn. "ORD-20250107-001").
			order.TrackingNumber = GenerateOrderNumber.CreateOrderNumber();

			// 'TotalAmount' hesaplamasını burada yapıyoruz.
			order.TotalAmount = dto.Items.Sum(item => item.Quantity * item.UnitPrice);


			// OrderDetails oluşturma
			var orderDetails = dto.Items.Select(item => _mapper.Map<OrderDetail>(item)).ToList();
			var createdOrder = await _orderRepository.CreateAsync(order);
			await _unitOfWork.CommitAsync();

			var result = _mapper.Map<OrderDto>(createdOrder);

			return result;


		}
	}
}