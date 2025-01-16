using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Service.DTOs.OrderDtos;
using Service.DTOs.OrderAdminDtos;
using Service.DTOs.OrderDtos;
using Service.DTOs.ProductDtos;
using Service.DTOs.ResponseDtos;
using Service.Services.OrderAdminService;
using Service.Services.OrderService;
using Service.Services.ProductService;
using Web.API.Hubs;
using Service.DTOs.PaginationDto;

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

		[HttpGet]
		[Route("all")]
		public async Task<ResponseDto> GetAll()
		{
			var order = await _service.GetAllAsync();
			var orderDtos = _mapper.Map<List<OrderDto>>(order.ToList());
			return ResponseBuilder.CreateResponse(orderDtos, true, "Başarılı");
		}

		[HttpGet("{id}")]
		public async Task<ResponseDto> GetById(int id)
		{
			var order = await _service.GetByIdAsync(id);
			var orderDto = _mapper.Map<OrderDto>(order);
			return ResponseBuilder.CreateResponse(orderDto, true, "Başarılı");
		}

		[HttpPost]
		[Route("GetPaged")]
		public async Task<ResponseDto> Page(PaginationDto paginationDto)
		{
			var data = await _service.GetPagedAsync(paginationDto);

			return ResponseBuilder.CreateResponse(data, true, "Başarılı");
		}

		[HttpDelete("{id}")]
		public async Task<ResponseDto> Delete(int id)
		{
			var data = await _service.DeleteAsync(id);

			return ResponseBuilder.CreateResponse(data, true, "Başarılı");

		}

		[HttpPost]
		[Route("DeleteRange")]
		public async Task<ActionResult> DeleteRange([FromBody] IEnumerable<int> entityIds)
		{
			int deletedCount = 0;
			foreach (var id in entityIds)
			{
				if (await _service.DeleteAsync(id))
				{
					deletedCount++;
				}
			}
			return Ok($"{deletedCount} öğe silindi.");
		}

	}
}
