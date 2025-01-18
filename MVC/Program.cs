using MVC.Configuration;
using MVC.Extensions;

namespace MVC
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

			// Add services to the container.
			builder.Services.Configure<UrlConfiguration>(builder.Configuration.GetSection("UrlConfiguration"));
			builder.Services.AddControllersWithViews();			
			builder.Services.AddServicesExtension();
			builder.Services.AddHttpContextAccessor();
			builder.Services.AddHttpClient();



			var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();


            // Varsay�lan olarak Admin Paneline y�nlendirme
            app.Use(async (context, next) =>
            {
                if (context.Request.Path == "/")
                {
                    context.Response.Redirect("/admin/home");
                }
                else
                {
                    await next();
                }
            });

            // Admin alan� i�in route
            app.MapControllerRoute(
                name: "areas",
                pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

            // Default route
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
