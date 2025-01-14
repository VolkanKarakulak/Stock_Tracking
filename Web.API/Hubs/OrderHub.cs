using Microsoft.AspNetCore.SignalR;

namespace Web.API.Hubs
{
	public class OrderHub : Hub
	{
		// Yeni sipariş bildirimi gönderme metodu
		public async Task SendMessage(string orderId)
		{
			await Clients.All.SendAsync("ReceiveOrder", orderId);
		}
	}
}
