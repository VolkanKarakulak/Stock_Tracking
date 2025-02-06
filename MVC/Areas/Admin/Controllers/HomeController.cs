using Microsoft.AspNetCore.Mvc;
using MVC.Areas.Admin.PageViewModels.HomePageViewModels;
using MVC.Services.OrderService;
using Newtonsoft.Json;

namespace MVC.Areas.Admin.Controllers
{
	[Area("Admin")]
	public class HomeController : Controller
	{
        private readonly IOrderService _orderService;

        public HomeController(IOrderService orderService)
        {
            _orderService = orderService;
        }
        public async Task<IActionResult> Index()
		{
            // Pending sipariş sayısını alıyoruz
            var pendingOrdersCount = await _orderService.GetPendingOrdersCountAsync();
            var todayOrdersCount = await _orderService.GetTodayOrdersCountAsync();
            var dailyEarnings = await _orderService.GetDailyEarningsAsync();
            var dailyEarningsResult = @dailyEarnings.ToString("N2");
            var totalOrdersCount = await _orderService.GetTotalOrdersAsync();
            var lastTenOrders = await _orderService.GetLastTenOrdersAsync();

            // Aylık kazançları alıyoruz
            var monthlyEarnings = await _orderService.CalculateMonthlyEarningsAsync();

            var viewModel = new HomePageViewModel()
            {
                PendingOrdersCount = pendingOrdersCount,
                TodayOrdersCount = todayOrdersCount,
                DailyEarnings = dailyEarningsResult,
                TotalOrdersCount = totalOrdersCount,
                LastTenOrders = lastTenOrders,
                MonthlyEarnings = monthlyEarnings
            };

            //// Pending sipariş sayısını View'a gönderiyoruz
            //ViewBag.PendingOrdersCount = pendingOrdersCount;
            //ViewBag.TodayOrdersCount = todayOrdersCount;
            //ViewBag.DailyEarnings = dailyEarningsResult;
            //ViewBag.TotalOrdersCount = totalOrdersCount;
            //ViewBag.LastTenOrders = lastTenOrders;

            //// Aylık kazançları JSON formatında View'a gönderiyoruz
            //ViewBag.MonthlyEarnings = JsonConvert.SerializeObject(monthlyEarnings);

            return View(viewModel);
		}
	}
}
