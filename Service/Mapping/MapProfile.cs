using AutoMapper;
using Data.Entities;
using Service.DTOs.CategoryDtos;
using Service.DTOs.ProductDtos;
using Service.DTOs.ProductStockDtos;

namespace Service.Mapping
{
    public class MapProfile : Profile
    {
        public MapProfile()
        {
            CreateMap<ProductDto, Product>().ReverseMap();
            CreateMap<ProductUpdateDto, Product>().ReverseMap();
            CreateMap<ProductAddDto, Product>().ReverseMap();

            CreateMap<CategoryDto, Category>().ReverseMap();
            CreateMap<CategoryUpdateDto, Category>().ReverseMap();
            CreateMap<CategoryAddDto, Category>().ReverseMap();

            CreateMap<ProductStockDto, ProductStock>().ReverseMap();
            CreateMap<ProductStockUpdateDto, ProductStock>().ReverseMap();
            CreateMap<ProductStockAddDto, ProductStock>().ReverseMap();

        }
    }
}
