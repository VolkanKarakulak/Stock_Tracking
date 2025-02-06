using AutoMapper;
using Data.Entities;
using MVC.Models.CategoryModels;
using MVC.Models.OrderModels;
using Service.DTOs.CategoryDtos;
using Service.DTOs.OrderDtos;

namespace MVC.Mapping
{
	public class MapProfile : Profile
	{

		public MapProfile()
		{
			CreateMap<CategoryModel, Category>().ReverseMap();
			CreateMap<CategoryUpdateModel, Category>().ReverseMap();
			CreateMap<CategoryAddModel, Category>().ReverseMap();

            CreateMap<OrderModel, Category>().ReverseMap();
            CreateMap<OrderUpdateModel, Category>().ReverseMap();
            CreateMap<Order, LastTenOrdersModel>()
                .ForMember(dest => dest.CustomerName, opt => opt
                .MapFrom(src => src.Customer != null ? src.Customer.Name : "Unknown"))
                .ReverseMap();


        }
	}
}
