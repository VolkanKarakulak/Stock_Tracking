using AutoMapper;
using Data.Entities;
using Data.Repositories.CategoryRepositories;
using Data.Repositories.GenericRepositories;
using Data.UnitOfWorks;
using Service.DTOs.CategoryDtos;
using Service.DTOs.PaginationDto;
using Service.DTOs.ResponseDto;
using Service.Services.GenericService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services.CategoryService
{
    public class CategoryService : GenericService<Category, CategoryDto>, ICategoryService
    {
        private readonly IMapper _mapper;
        private readonly ICategoryRepository _categoryRepository;
        public CategoryService(IGenericRepository<Category> repository, IUnitOfWork unitOfWork, IMapper mapper, ICategoryRepository categoryRepository) : base(repository, unitOfWork, mapper)
        {
            _mapper = mapper;
            _categoryRepository = categoryRepository;
        }

        //public async Task<CategoryDto> CreateCategoryAsync(CategoryAddDto entity)
        //{
        //    var category = _mapper.Map<Category>(entity);
        //    var categoryCreateResult = _categoryRepository.CreateAsync(category);



       
    }
}
