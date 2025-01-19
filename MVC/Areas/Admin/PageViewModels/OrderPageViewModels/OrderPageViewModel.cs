using MVC.Models.CategoryModels;
using MVC.Models.OrderModels;
using MVC.Models.PagedResponseModel;

namespace MVC.Areas.Admin.PageViewModels.OrderPageViewModels
{
    public class OrderPageViewModel
    {
        public PagedResponseModel<IEnumerable<OrderModel>> Orders { get; set; }
    }
}
