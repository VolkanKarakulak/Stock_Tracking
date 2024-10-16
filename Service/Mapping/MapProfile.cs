using AutoMapper;
using Data.Entities;
using Service.DTOs.CategoryDtos;
using Service.DTOs.ProductDtos;

namespace Service.Mapping
{
    public class MapProfile : Profile
    {
        public MapProfile()
        {
            CreateMap<ProductDto, Product>().ReverseMap();
            CreateMap<ProductUpdateDto, Product>().ReverseMap();
            CreateMap<ProductAddDto, Product>().ReverseMap();


            CreateMap<Category, CategoryDto>().ReverseMap();

        }
    }
}
