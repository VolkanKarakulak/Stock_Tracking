using MVC.Models.CategoryModels;
using MVC.Models.PagedResponseModel;

namespace MVC.Areas.Admin.PageViewModels.CategoryPageViewModels
{
	public class CategoryPageViewModel
	{
		public PagedResponseModel<IEnumerable<CategoryModel>> Categories { get; set; }
	}
}
