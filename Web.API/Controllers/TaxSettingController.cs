using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Service.DTOs.CategoryDtos;
using Service.DTOs.OrderAdminDtos;
using Service.DTOs.PaginationDto;
using Service.DTOs.ProductStockDtos;
using Service.DTOs.ResponseDtos;
using Service.DTOs.TaxSettingDtos;
using Service.Services.CategoryService;
using Service.Services.TaxSettingService;

namespace Web.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class TaxSettingController : ControllerBase
	{
		private readonly IMapper _mapper;
		private readonly ITaxSettingService _service;

		public TaxSettingController(IMapper mapper, ITaxSettingService service)
		{
			_mapper = mapper;
			_service = service;
		}

		[HttpGet]
		[Route("all")]
		public async Task<ResponseDto> GetAll()
		{
			var taxSettings = await _service.GetAllAsync();
			var taxSettingDto = _mapper.Map<List<TaxSettingDto>>(taxSettings.ToList());
			return ResponseBuilder.CreateResponse(taxSettingDto, true, "Başarılı");
		}

		[HttpGet("{id}")]
		public async Task<ResponseDto> GetById(int id)
		{
			var taxSetting = await _service.GetByIdAsync(id);
			var taxSettingDto = _mapper.Map<TaxSettingDto>(taxSetting);
			return ResponseBuilder.CreateResponse(taxSettingDto, true, "Başarılı");
		}

		[HttpPost]
		public async Task<ResponseDto> Create(TaxSettingAddDto taxSettingAddDto)
		{
			var taxSettingDto = await _service.CreateAsync(_mapper.Map<TaxSettingDto>(taxSettingAddDto));
			return ResponseBuilder.CreateResponse(taxSettingDto, true, "Başarılı");
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


		[HttpPost]
		[Route("GetPaged")]
		public async Task<ResponseDto> Page(PaginationDto paginationDto)
		{
			var data = await _service.GetPagedAsync(paginationDto);

			return ResponseBuilder.CreateResponse(data, true, "Başarılı");
		}

		[HttpPut]
		public async Task<ResponseDto> Update(TaxSettingUpdateDto dto)
		{
			var taxSettingDto = _mapper.Map<TaxSettingDto>(dto);
			var taxSetting = await _service.UpdateAsync(taxSettingDto);
			return ResponseBuilder.CreateResponse(taxSetting, true, "Başarılı");
		}
	}
}
