using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Service.DTOs.OrderAdminDtos;
using Service.DTOs.OrderDtos;
using Service.DTOs.ProductDtos;
using Service.DTOs.ResponseDtos;
using Service.Services.OrderAdminService;
using Service.Services.OrderService;
using Service.Services.ProductService;
using Web.API.Hubs;

namespace Web.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class OrderController : ControllerBase
	{
		private readonly IMapper _mapper;
		private readonly IOrderService _service;
		private readonly IOrderAdminService _adminService;
		private readonly IHubContext<OrderHub> _hubContext;

		public OrderController(IOrderService service, IMapper mapper, IOrderAdminService adminService, IHubContext<OrderHub> hubContext)
		{
			_service = service;
			_mapper = mapper;
			_adminService = adminService;
			_hubContext = hubContext;


		}

		[HttpPost]
		public async Task<ResponseDto> Create(OrderAddDto orderAddDto)
		{
			var orderDto = await _service.CreateOrderAsync(orderAddDto);

			// SignalR ile bildirim gönder
			await _hubContext.Clients.All.SendAsync("ReceiveOrder", orderDto.TrackingNumber, "Yeni bir sipariş alındı!");

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
