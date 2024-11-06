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

            // ProductStock'tan Product'a mapleme
            CreateMap<ProductStock, Product>()
                .ForMember(dest => dest.Stock, opt => opt.MapFrom(src => src.Quantity)) // Product'ın Stock alanına ProductStock'tan Quantity'yi atıyoruz
                .ForMember(dest => dest.IsActive, opt => opt.MapFrom(src => src.IsActive))
                .ForMember(dest => dest.Id, opt => opt.Ignore()); // Product'ın Id'sini maplemiyoruz

            // Product'tan ProductStock'a mapleme
            CreateMap<Product, ProductStock>()
                .ForMember(dest => dest.ProductId, opt => opt.MapFrom(src => src.Id)) // ProductStock'un ProductId alanını Product'ın Id'si ile eşleştiriyoruz
                .ForMember(dest => dest.Id, opt => opt.Ignore()); // ProductStock'un kendi Id'sini maplemiyoruz


        }
    }
}
