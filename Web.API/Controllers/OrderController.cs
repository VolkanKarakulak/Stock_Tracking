using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Service.DTOs.OrderDtos;
using Service.DTOs.ResponseDtos;
using Service.Services.OrderService;
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
		private readonly IHubContext<OrderHub> _hubContext;

		public OrderController(IOrderService service, IMapper mapper, IHubContext<OrderHub> hubContext)
		{
			_service = service;
			_mapper = mapper;
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
		public async Task<ResponseDto> Update(OrderUpdateDto dto)
		{
			var orderDto = await _service.UpdateOrderAsync(dto);
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

        [HttpGet("pending-orders-count")]
        public async Task<IActionResult> GetPendingOrdersCount()
        {
            var count = await _service.GetPendingOrdersCountAsync();
            return Ok(count);
        }

        [HttpGet("today-orders-count")]
        public async Task<IActionResult> GetTodayOrdersCount()
        {
            var count = await _service.GetTodayOrdersCountAsync();
            return Ok(count);
        }
    }
}
