using AutoMapper;
using Entity.Concrete;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.Abstract;
using Service.DTOs.ProductDtos;
using Service.DTOs.ResponseDto;

namespace Web.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController :  CustomBaseController
    {
        private readonly IMapper _mapper;
        private readonly IGenericService<Product> _service;
        public ProductsController(IGenericService<Product> service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        [HttpGet]
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
        public async Task<IActionResult> Create(ProductDto productDto)
        {
            var product = await _service.CreateAsync(_mapper.Map<Product>(productDto));
            var productsDto = _mapper.Map<ProductDto>(product);
            return CreateActionResult(CustomResponseDto<ProductDto>.Success(200, productsDto));
        }

        [HttpPut]
        public async Task<IActionResult> Update(ProductUpdateDto productUpdateDto)
        {
            await _service.UpdateAsync(_mapper.Map<Product>(productUpdateDto));  
            return CreateActionResult(CustomResponseDto<EmptyContentDto>.Success(204));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var product = await _service.GetByIdAsync(id);

            await _service.DeleteAsync(product);
   
            return CreateActionResult(CustomResponseDto<EmptyContentDto>.Success(204));
        }


    }
}
