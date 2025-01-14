using AutoMapper;
using Data.Entities;
using Service.DTOs.CategoryDtos;
using Service.DTOs.OrderDtos;
using Service.DTOs.OrderIDetailDtos;
using Service.DTOs.OrderItemDtos;
using Service.DTOs.ProductDtos;
using Service.DTOs.ProductStockDtos;
using Service.DTOs.SupplierDtos;
using Service.DTOs.TaxSettingDtos;

namespace Service.Mapping
{
    public class MapProfile : Profile
    {
        public MapProfile()
        {
            CreateMap<ProductDto, Product>().ReverseMap();
            CreateMap<ProductUpdateDto, Product>().ReverseMap();
            CreateMap<ProductAddDto, Product>().ReverseMap();
            CreateMap<Product, ProductDto>()
                .ForMember(dest => dest.CategoryIds, opt => opt
                .MapFrom(src => src.ProductCategories!.Select(pc => pc.CategoryId)))
                .ForMember(dest => dest.SupplierIds, opt => opt
                .MapFrom(src => src.ProductSuppliers!.Select(ps => ps.SupplierId)));


            CreateMap<SupplierDto, Supplier>().ReverseMap();
			CreateMap<SupplierUpdateDto, Supplier>().ReverseMap();
			CreateMap<SupplierAddDto, Supplier>().ReverseMap();


			CreateMap<TaxSettingDto, TaxSetting>().ReverseMap();
			CreateMap<TaxSettingUpdateDto, TaxSetting>().ReverseMap(); 
			CreateMap<TaxSettingAddDto, TaxSetting>().ReverseMap();
			CreateMap<TaxSettingAddDto, TaxSettingDto>().ReverseMap();
            CreateMap<TaxSettingUpdateDto, TaxSettingDto>().ReverseMap();

			CreateMap<CategoryDto, Category>().ReverseMap();
            CreateMap<CategoryUpdateDto, Category>().ReverseMap();
            CreateMap<CategoryAddDto, Category>().ReverseMap();

            CreateMap<ProductStockDto, ProductStock>()
                .ForMember(dest => dest.CreatedDate, opt => opt.Ignore()) // Ignore CreatedDate during mapping
                .ReverseMap();

            CreateMap<ProductStockUpdateDto, ProductStock>()
                .ForMember(k => k.IsActive, l => l.MapFrom(m => m.IsActive)) // IsActive özelliğini eşleştir
                .ForMember(k => k.Quantity, l => l.MapFrom(m => m.Quantity)) // Quantity özelliğini eşleştir
                .ForMember(k => k.ProductId, l => l.MapFrom(m => m.ProductId))
                .ForMember(k => k.CreatedDate, opt => opt.Ignore())
                .ReverseMap();

            CreateMap<ProductStock, ProductUpdateDto>()
                .ForMember(k => k.Id, l => l.MapFrom(m => m.ProductId))               
                .ForMember(k => k.IsActive, l => l.MapFrom(m => m.IsActive))
                .ForMember(k => k.Description, opt => opt.Ignore())
                .ForMember(k => k.Stock, l => l.MapFrom(m => m.Quantity))
                .ReverseMap();

            CreateMap<ProductStockAddDto, ProductStock>()
                .ForMember(k => k.Quantity, l => l.MapFrom(m => m.Quantity)) // Product tablosundaki Stock alanına Quantity'yi atıyoruz
                .ForMember(k => k.ProductId, l => l.MapFrom(m => m.ProductId))
                .ReverseMap().ReverseMap();

            CreateMap<ProductStockUpdateDto, Product>()
                .ForMember(k => k.Stock, l => l.MapFrom(m => m.Quantity)) // Product tablosundaki Stock alanına Quantity'yi atıyoruz
                .ForMember(k => k.IsActive, l => l.MapFrom(m => m.IsActive))
                .ForMember(k => k.CreatedDate, opt => opt.Ignore())
                .ForMember(k => k.Id, l => l.MapFrom(m => m.ProductId)) 
                .ReverseMap();
            
            CreateMap<ProductAddDto, ProductStock>()
                .ForMember(k => k.Quantity, l => l.MapFrom(m => m.Stock)) // Product tablosundaki Stock alanına Quantity'yi atıyoruz
                .ForMember(k => k.CreatedDate, opt => opt.Ignore()) 
                .ReverseMap();

            // ProductStock'tan Product'a mapleme
            CreateMap<ProductStock, Product>()
                .ForMember(k => k.Stock, l => l.MapFrom(m => m.Quantity)) // Product'ın Stock alanına ProductStock'tan Quantity'yi atıyoruz
                .ForMember(k => k.IsActive, l => l.MapFrom(m => m.IsActive))
                .ForMember(k => k.Id, l => l.Ignore()); // Product'ın Id'sini maplemiyoruz

            // Product'tan ProductStock'a mapleme
            CreateMap<Product, ProductStock>()
                .ForMember(k => k.ProductId, l => l.MapFrom(m => m.Id)) // ProductStock'un ProductId alanını Product'ın Id'si ile eşleştiriyoruz
                .ForMember(k => k.Quantity, l => l.MapFrom(m => m.Stock))
                .ForMember(k => k.Id, l => l.Ignore()) // ProductStock'un kendi Id'sini maplemiyoruz
                .ForMember(k => k.Description, l => l.Ignore()) 
                .ForMember(k => k.CreatedDate, l => l.Ignore()) 
                .ForMember(k => k.UpdatedDate, l => l.Ignore()) 
                .ForMember(k => k.IsDeleted, l => l.Ignore());


            CreateMap<ProductStock, ProductDto>().ReverseMap();
            CreateMap<ProductCategory, ProductDto>().ReverseMap();
              
            CreateMap<ProductStockDto, CategoryDto>().ReverseMap();
            CreateMap<CategoryAddDto, CategoryDto>();

            CreateMap<Category, ProductStockDto>().ReverseMap();

			CreateMap<Order, OrderDto>()
	        .ForMember(dest => dest.Items, opt => opt.MapFrom(src => src.OrderDetails));
			CreateMap<Order, OrderAddDto>().ReverseMap();

            CreateMap<OrderAddDto, Order>()
           .ForMember(dest => dest.CreatedDate, opt => opt.MapFrom(src => DateTime.Now))
           .ForMember(dest => dest.OrderDetails, opt => opt.MapFrom(src => src.Items))
           .ForMember(dest => dest.Status, opt => opt.MapFrom(src => "Pending")); 
	

			CreateMap<OrderDetail, OrderDetailAddDTO>().ReverseMap();
			CreateMap<OrderDetail, OrderDetailDto>().ReverseMap();
			CreateMap<OrderDetailAddDTO, OrderDetail>()
		        .ForMember(dest => dest.UnitPrice, opt => opt
                .MapFrom(src => src.Quantity * src.UnitPrice)); // Toplam fiyat hesaplanıyor
		}
    }
}
