using Data.Entities;
using Data.Repositories.GenericRepositories;
using Data.UnitOfWorks;
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
    public class CategoryService : GenericService<Category>, ICategoryService
    {
        public CategoryService(IGenericRepository<Category> repository, IUnitOfWork unitOfWork) : base(repository, unitOfWork)
        {
        }
    }
}
