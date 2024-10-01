using AutoMapper;
using Entity.Concrete;
using Service.DTOs.CategoryDtos;
using Service.DTOs.ProductDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Mapping
{
    public class MapProfile : Profile
    {
        public MapProfile()
        {
            CreateMap<Product, ProductDto>().ReverseMap();
            CreateMap<ProductUpdateDto, Product>();
            CreateMap<Category, CategoryDto>().ReverseMap();

        }
    }
}
