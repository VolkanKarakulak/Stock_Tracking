using Microsoft.AspNetCore.Mvc;
using MVC.Areas.Admin.PageViewModels.OrderPageViewModels;
using MVC.Models.PaginationModel;
using MVC.Services.OrderService;

namespace MVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    //[Authorize(Roles = "Admin")]
    public class OrderController : Controller
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        public async Task<IActionResult> Index(int pageNumber = 1, int pageSize = 12)
        {
            var paginationModel = new PaginationModel()
            {
                PageNumber = pageNumber,
                PageSize = pageSize
            };
            var order = await _orderService.GetPagedAsync(paginationModel);
            var orderPageViewModel = new OrderPageViewModel()
            {
                Orders = order.Data,
            };

            return View(orderPageViewModel);
        }
    }
}
