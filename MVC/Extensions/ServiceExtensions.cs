using MVC.Services.CategoryService;
using MVC.Services.OrderService;


namespace MVC.Extensions
{
	public static class ServiceExtensions
	{
		public static void AddServicesExtension(this IServiceCollection services)
		{
			services.AddScoped<ICategoryService, CategoryService>();
			services.AddScoped<IOrderService, OrderService>();
		}
	}
}
