using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Service.DTOs.OrderDtos;
using Service.DTOs.ProductDtos;
using Service.DTOs.ResponseDtos;
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
		public OrderController(IOrderService service, IMapper mapper)
		{
			_service = service;
			_mapper = mapper;
		}

		[HttpPost]
		public async Task<ResponseDto> Create(OrderAddDto orderAddDto)
		{
			var orderDto = await _service.CreateOrderAsync(orderAddDto);
			return ResponseBuilder.CreateResponse(orderDto, true, "Başarılı");
		}
	}
}
