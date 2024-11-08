using AutoMapper;
using Data.Entities;
using Microsoft.AspNetCore.Mvc;
using Service.DTOs.ProductStockDtos;
using Service.DTOs.PaginationDto;
using Service.DTOs.ResponseDtos;
using Service.Services.ProductStockService;


namespace Web.API.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class ProductStockController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IProductStockService _service;

        public ProductStockController(IMapper mapper, IProductStockService service)
        {
            _mapper = mapper;
            _service = service;
        }

        [HttpGet]
        [Route("all")]
        public async Task<ResponseDto> GetAll()
        {
            var productStock = await _service.GetAllAsync();
            var productStockDtos = _mapper.Map<List<ProductStockDto>>(productStock.ToList());
            return ResponseBuilder.CreateResponse(productStockDtos, true, "Başarılı");
        }

        [HttpGet("{id}")]
        public async Task<ResponseDto> GetById(int id)
        {
            var productStock = await _service.GetByIdAsync(id);
            var productStockDto = _mapper.Map<ProductStockDto>(productStock);
            return ResponseBuilder.CreateResponse(productStockDto, true, "Başarılı");
        }

        [HttpPost]
        public async Task<ResponseDto> Create(ProductStockAddDto productStockAddDto)
        {
            var productStockDto = await _service.CreateProductStockAsync(productStockAddDto);
            return ResponseBuilder.CreateResponse(productStockDto, true, "Başarılı");
        }


        //[HttpPost]
        //[Route("CreateRange")]
        //public async Task<ResponseDto> CreateRange(IEnumerable<ProductStockAddDto> productStockAddDto)
        //{
        //    var productStock = await _service.CreateRangeAsync(_mapper.Map<IEnumerable<ProductStock>>(productStockAddDto));
        //    var productStockDto = _mapper.Map<IEnumerable<ProductStockDto>>(productStock);
        //    return ResponseBuilder.CreateResponse(productStockDto, true, "Başarılı");
        //}

        //[HttpPut]
        //public async Task<ResponseDto> Update(ProductStockUpdateDto productStockUpdateDto)
        //{
        //    await _service.UpdateAsync(_mapper.Map<ProductStock>(productStockUpdateDto));
        //    return ResponseBuilder.CreateResponse(productStockUpdateDto, true, "Başarılı");
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
