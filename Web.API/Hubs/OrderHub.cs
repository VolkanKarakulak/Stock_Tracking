﻿using Microsoft.AspNetCore.SignalR;
using Service.DTOs.OrderDtos;
using Service.Services.OrderService;

namespace Web.API.Hubs
{
	public class OrderHub : Hub
	{
		private readonly IOrderService _orderService;

        public OrderHub(IOrderService orderService)
        {
            _orderService = orderService;
        }

        // Yeni sipariş bildirimi gönderme metodu
        public async Task SendOrderUpdate(OrderDto order)
		{
			var pendingOrdersCount = await _orderService.GetPendingOrdersCountAsync();
			var todayOrdersCount = await _orderService.GetTodayOrdersCountAsync();
			await Clients.All.SendAsync("ReceiveOrder", order, pendingOrdersCount, todayOrdersCount);
		}

	}
}
