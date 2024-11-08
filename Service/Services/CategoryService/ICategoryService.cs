using Data.Entities;
using Service.DTOs.CategoryDtos;
using Service.Services.GenericService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services.CategoryService
{
    public interface ICategoryService : IGenericService<Category, CategoryDto>
    {
    }
}
