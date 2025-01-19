using MVC.Models.CategoryModels;
using MVC.Models.PagedResponseModel;
using MVC.Models.PaginationModel;
using MVC.Models.ResponseModels;
using System.Linq.Expressions;

namespace MVC.Services.CategoryService
{
    public interface ICategoryService
	{
		Task<ResponseModel<CategoryModel>> GetByIdAsync(int id);
		Task<ResponseModel<IEnumerable<CategoryModel>>> GetAllAsync();
		Task<ResponseModel<CategoryModel>> CreateAsync(CategoryAddModel model);
		Task<ResponseModel<CategoryModel>> UpdateAsync(CategoryUpdateModel model);
		Task<ResponseModel<NoDataModel>> DeleteAsync(int id);
		Task<ResponseModel<PagedResponseModel<IEnumerable<CategoryModel>>>> GetPagedAsync(PaginationModel paginationModel);
	}
}
