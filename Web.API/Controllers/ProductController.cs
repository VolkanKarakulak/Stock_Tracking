using AutoMapper;
using Data.Entities;
using Microsoft.AspNetCore.Mvc;
using Service.DTOs.ProductDtos;
using Service.DTOs.ResponseDto;
using Service.DTOs.ResponseDtos;
using Service.Exceptions.NotFoundExeptions;
using Service.Services.ProductService;

namespace Web.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController :  CustomBaseController
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
        public async Task<IActionResult> GetAll()
        {
            var product = await _service.GetAllAsync();
            var productsDtos = _mapper.Map<List<ProductDto>>(product.ToList());
            //return Ok(CustomResponseDto<List<ProductDto>>.Success(200, productsDtos));
            return CreateActionResult(CustomResponseDto<List<ProductDto>>.Success(200, productsDtos));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var product = await _service.GetByIdAsync(id);
            var productsDto = _mapper.Map<ProductDto>(product);
            return CreateActionResult(CustomResponseDto<ProductDto>.Success(200, productsDto));
        }

        [HttpPost]
        public async Task<IActionResult> Create(ProductAddDto productAddDto)
        {
            var product = await _service.CreateAsync(_mapper.Map<Product>(productAddDto));
            var productDto = _mapper.Map<ProductDto>(product);
            return CreateActionResult(CustomResponseDto<ProductDto>.Success(200, productDto));
        }

        [HttpPut]
        public async Task<IActionResult> Update(ProductUpdateDto productUpdateDto)
        {
            await _service.UpdateAsync(_mapper.Map<Product>(productUpdateDto));  
            return CreateActionResult(CustomResponseDto<ProductUpdateDto>.Success(200, productUpdateDto));
        }

        [HttpDelete("{id}")]
        public async Task<ResponseDto> Delete(int id)
        {
            var result = await _service.DeleteAsync(id);

            return new ResponseDto
            {
                Data = result,
                StatusCode = 204,
                Message = "Başarılı",
                IsSuccess = true
            };
        }
    }
}
