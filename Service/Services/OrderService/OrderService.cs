using AutoMapper;
using Data.Entities;
using Data.Repositories.GenericRepositories;
using Data.Repositories.OrderRepositories;
using Data.UnitOfWorks;
using Service.DTOs.OrderDtos;
using Service.DTOs.PaginationDto;
using Service.DTOs.ResponseDto;
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
		public OrderService(IGenericRepository<Order> repository, IUnitOfWork unitOfWork, IMapper mapper, IOrderRepository orderRepository) : base(repository, unitOfWork, mapper)
		{
			_mapper = mapper;
			_orderRepository = orderRepository;
		}
	}
}
