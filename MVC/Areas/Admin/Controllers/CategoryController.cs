using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MVC.Areas.Admin.PageViewModels.CategoryPageViewModels;
using MVC.Models.PaginationModel;
using MVC.Services.CategoryServices;
using Service.DTOs.PaginationDto;

namespace MVC.Areas.Admin.Controllers
{

	[Area("Admin")]
	//[Authorize(Roles = "Admin")]
	public class CategoryController : Controller
	{
		private readonly ICategoryService _categoryService;
		public CategoryController(ICategoryService CategoryService)
		{
			_categoryService = CategoryService;
		}
		public async Task<IActionResult> Index(int pageNumber = 1, int pageSize = 12)
		{
			var paginationModel = new PaginationModel()
			{
				PageNumber = pageNumber,
				PageSize = pageSize
			};
			var category = await _categoryService.GetPagedAsync(paginationModel);
			var categoryPageViewModel = new CategoryPageViewModel()
			{
				Categories = category.Data,
			};


			return View(categoryPageViewModel);
		}
	}
}
