using AutoMapper;
using Data.Entities;
using Microsoft.AspNetCore.Mvc;
using Service.DTOs.PaginationDto;
using Service.DTOs.ProductDtos;
using Service.DTOs.ResponseDto;
using Service.DTOs.ResponseDtos;
using Service.Exceptions.NotFoundExeptions;
using Service.Services.ProductService;

namespace Web.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController :  ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IProductService _service;
        public ProductController(IProductService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("all")]
        public async Task<ResponseDto> GetAll()
        {
            var product = await _service.GetAllAsync();
            var productDtos = _mapper.Map<List<ProductDto>>(product.ToList());
            //return Ok(CustomResponseDto<List<ProductDto>>.Success(200, productsDtos));
            return ResponseBuilder.CreateResponse(productDtos, true, "Başarılı");
        }
    
        [HttpGet("{id}")]
        public async Task<ResponseDto> GetById(int id)
        {
            var product = await _service.GetByIdAsync(id);
            var productDto = _mapper.Map<ProductDto>(product);
            return ResponseBuilder.CreateResponse(productDto, true, "Başarılı");
        }

        [HttpPost]
        public async Task<ResponseDto> Create(ProductAddDto productAddDto)
        {
            var product = await _service.CreateAsync(_mapper.Map<Product>(productAddDto));
            var productDto = _mapper.Map<ProductDto>(product);
            return ResponseBuilder.CreateResponse(productDto, true, "Başarılı");
        }

        [HttpPost]
        [Route("CreateRange")]
        public async Task<ResponseDto> CreateRange(IEnumerable<ProductAddDto> productAddDtos)
        {
            var product = await _service.CreateRangeAsync(_mapper.Map<IEnumerable<Product>>(productAddDtos));
            var productDto = _mapper.Map<IEnumerable<ProductDto>>(product);
            return ResponseBuilder.CreateResponse(productDto, true, "Başarılı");
        }

        [HttpPut]
        public async Task<ResponseDto> Update(ProductUpdateDto productUpdateDto)
        {
            await _service.UpdateAsync(_mapper.Map<Product>(productUpdateDto));
            return ResponseBuilder.CreateResponse(productUpdateDto, true, "Başarılı");
        }

        [HttpDelete("{id}")]
        public async Task<ResponseDto> Delete(int id)
        {
            var result = await _service.DeleteAsync(id);
                    
            return ResponseBuilder.CreateResponse(result, true, "Başarılı");

        }

        [HttpPost]
        [Route("GetPagedByCategoryId")]
        public async Task<ResponseDto> Page(int categoryId, PaginationDto paginationDto)
        {
            var result = await _service.GetProductsByCategoryIdPagedAsync(categoryId, paginationDto);

            return ResponseBuilder.CreateResponse(result, true, "Başarılı");
        }
    }
    
}
