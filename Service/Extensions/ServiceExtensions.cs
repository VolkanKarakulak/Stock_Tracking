using Microsoft.Extensions.DependencyInjection;
using Service.Services.CategoryService;
using Service.Services.GenericService;
using Service.Services.OrderAdminService;
using Service.Services.OrderService;
using Service.Services.ProductService;
using Service.Services.ProductStockService;
using Service.Services.SupplierService;
using Service.Services.TaxSettingService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Service.Extensions
{
    public static class ServiceExtensions
    {
        public static void AddServiceExtensions(this IServiceCollection service) 
        { 

            service.AddScoped(typeof(IGenericService<,>), typeof(GenericService<,>));
            service.AddScoped<IProductService, ProductService>();
            service.AddScoped<ICategoryService, CategoryService>();
            service.AddScoped<IProductStockService, ProductStockService>();
            service.AddScoped<IOrderService, OrderService>();
            service.AddScoped<IOrderAdminService, OrderAdminService>();
            service.AddScoped<ITaxSettingService, TaxSettingService>();
            service.AddScoped<ISupplierService, SupplierService>();

        }

    }
}
