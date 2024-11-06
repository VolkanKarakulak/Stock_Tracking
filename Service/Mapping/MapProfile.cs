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
            CreateMap<ProductStockUpdateDto, ProductStock>()
                .ForMember(k => k.IsActive, l => l.MapFrom(m => m.IsActive)) // IsActive özelliğini eşleştir
                .ForMember(k => k.Quantity, l => l.MapFrom(m => m.Quantity)) // Quantity özelliğini eşleştir
                .ForMember(k => k.ProductId, l => l.MapFrom(m => m.ProductId))
                .ReverseMap(); 

            CreateMap<ProductStockAddDto, ProductStock>().ReverseMap();

            CreateMap<ProductStockUpdateDto, Product>()
                .ForMember(k => k.Stock, l => l.MapFrom(m => m.Quantity)) // Product tablosundaki Stock alanına Quantity'yi atıyoruz
                .ForMember(k => k.IsActive, l => l.MapFrom(m => m.IsActive))
                .ForMember(k => k.Id, l => l.MapFrom(m => m.ProductId)) 
                .ReverseMap();
        }
    }
}
