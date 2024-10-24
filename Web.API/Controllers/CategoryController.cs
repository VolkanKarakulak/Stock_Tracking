using AutoMapper;
using Data.Entities;
using Microsoft.AspNetCore.Mvc;
using Service.DTOs.PaginationDto;
using Service.DTOs.CategoryDtos;
using Service.DTOs.ResponseDtos;
using Service.Services.CategoryService;

namespace Web.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ICategoryService _service;

        public CategoryController(IMapper mapper, ICategoryService service)
        {
            _mapper = mapper;
            _service = service;
        }

        [HttpGet]
        [Route("all")]
        public async Task<ResponseDto> GetAll()
        {
            var category = await _service.GetAllAsync();
            var categoryDtos = _mapper.Map<List<CategoryDto>>(category.ToList());
            return ResponseBuilder.CreateResponse(categoryDtos, true, "Başarılı");
        }

        [HttpGet("{id}")]
        public async Task<ResponseDto> GetById(int id)
        {
            var category = await _service.GetByIdAsync(id);
            var categoryDto = _mapper.Map<CategoryDto>(category);
            return ResponseBuilder.CreateResponse(categoryDto, true, "Başarılı");
        }

        [HttpPost]
        public async Task<ResponseDto> Create(CategoryAddDto categoryAddDto)
        {
            var category = await _service.CreateAsync(_mapper.Map<Category>(categoryAddDto));
            var categoryDto = _mapper.Map<CategoryDto>(category);
            return ResponseBuilder.CreateResponse(categoryDto, true, "Başarılı");
        }

        [HttpPost]
        [Route("CreateRange")]
        public async Task<ResponseDto> CreateRange(IEnumerable<CategoryAddDto> categoryAddDtos)
        {
            var category = await _service.CreateRangeAsync(_mapper.Map<IEnumerable<Category>>(categoryAddDtos));
            var categoryDto = _mapper.Map<IEnumerable<CategoryDto>>(category);
            return ResponseBuilder.CreateResponse(categoryDto, true, "Başarılı");
        }

        [HttpPut]
        public async Task<ResponseDto> Update(CategoryUpdateDto categoryUpdateDto)
        {
            await _service.UpdateAsync(_mapper.Map<Category>(categoryUpdateDto));
            return ResponseBuilder.CreateResponse(categoryUpdateDto, true, "Başarılı");
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
    }
}
