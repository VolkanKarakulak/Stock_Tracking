using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Service.DTOs.OrderAdminDtos;
using Service.DTOs.OrderDtos;
using Service.DTOs.ProductDtos;
using Service.DTOs.ResponseDtos;
using Service.Services.OrderAdminService;
using Service.Services.OrderService;
using Service.Services.ProductService;

namespace Web.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class OrderController : ControllerBase
	{
		private readonly IMapper _mapper;
		private readonly IOrderService _service;
		private readonly IOrderAdminService _adminService;
		public OrderController(IOrderService service, IMapper mapper, IOrderAdminService adminService)
		{
			_service = service;
			_mapper = mapper;
			_adminService = adminService;
		}

		[HttpPost]
		public async Task<ResponseDto> Create(OrderAddDto orderAddDto)
		{
			var orderDto = await _service.CreateOrderAsync(orderAddDto);
			return ResponseBuilder.CreateResponse(orderDto, true, "Başarılı");
		}

		[HttpPut]
		[Route("Admin")]
		public async Task<ResponseDto> Update(OrderUpdateByAdminDto dto)
		{
			var orderDto = await _adminService.UpdateOrderAsync(dto);
			return ResponseBuilder.CreateResponse(orderDto, true, "Başarılı");
		}
	}
}
