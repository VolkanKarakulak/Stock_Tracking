using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Service.DTOs.SupplierDtos;
using Service.DTOs.PaginationDto;
using Service.DTOs.ResponseDtos;
using Service.Services.SupplierService;
using Data.Entities;



namespace Web.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class SupplierController : ControllerBase
	{
		private readonly IMapper _mapper;
		private readonly ISupplierService _service;

		public SupplierController(IMapper mapper, ISupplierService service)
		{
			_mapper = mapper;
			_service = service;
		}


		[HttpGet]
		[Route("all")]
		public async Task<ResponseDto> GetAll()
		{
			var supplier = await _service.GetAllAsync();
			var supplierDto = _mapper.Map<List<SupplierDto>>(supplier.ToList());
			return ResponseBuilder.CreateResponse(supplierDto, true, "Başarılı");
		}

		[HttpGet("{id}")]
		public async Task<ResponseDto> GetById(int id)
		{
			var supplier = await _service.GetByIdAsync(id);
			var supplierDto = _mapper.Map<SupplierDto>(supplier);
			return ResponseBuilder.CreateResponse(supplierDto, true, "Başarılı");
		}

		[HttpPost]
		public async Task<ResponseDto> Create(SupplierAddDto supplierAddDto)
		{
			var supplier = await _service.CreateSupplierAsync(_mapper.Map<SupplierAddDto>(supplierAddDto));
			var supplierDto = _mapper.Map<SupplierDto>(supplier);
			return ResponseBuilder.CreateResponse(supplierDto, true, "Başarılı");
		}


		//[HttpPut]
		//public async Task<ResponseDto> Update(SupplierUpdateDto supplierUpdateDto)
		//{
		//	var supplier = await _service.UpdateAsync(_mapper.Map<SupplierUpdateDto>(supplierUpdateDto));
		//	var supplierDto = _mapper.Map<SupplierDto>(supplier);
		//	return ResponseBuilder.CreateResponse(supplierDto, true, "Başarılı");
		//}

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


		[HttpPost]
		[Route("GetPaged")]
		public async Task<ResponseDto> Page(PaginationDto paginationDto)
		{
			var data = await _service.GetPagedAsync(paginationDto);

			return ResponseBuilder.CreateResponse(data, true, "Başarılı");
		}
	}

}
