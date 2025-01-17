using AutoMapper;
using Data.Entities;
using Service.DTOs.CategoryDtos;

namespace MVC.Mapping
{
	public class MapProfile : Profile
	{

		public MapProfile()
		{
			CreateMap<CategoryDto, Category>().ReverseMap();
			CreateMap<CategoryUpdateDto, Category>().ReverseMap();
			CreateMap<CategoryAddDto, Category>().ReverseMap();
		}
	}
}
