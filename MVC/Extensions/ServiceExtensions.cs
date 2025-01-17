using MVC.Services.CategoryServices;

namespace MVC.Extensions
{
	public static class ServiceExtensions
	{
		public static void AddServicesExtension(this IServiceCollection services)
		{
			services.AddScoped<ICategoryService, CategoryService>();
		}
	}
}
