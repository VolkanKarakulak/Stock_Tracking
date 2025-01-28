﻿using Microsoft.AspNetCore.Mvc;
using MVC.Services.OrderService;

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

            // Pending sipariş sayısını View'a gönderiyoruz
            ViewBag.PendingOrdersCount = pendingOrdersCount;
            return View();
		}
	}
}
