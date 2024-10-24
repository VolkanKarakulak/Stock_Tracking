using Microsoft.Extensions.DependencyInjection;
using Service.Services.CategoryService;
using Service.Services.GenericService;
using Service.Services.ProductService;
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

            service.AddScoped(typeof(IGenericService<>), typeof(GenericService<>));
            service.AddScoped<IProductService, ProductService>();
            service.AddScoped<ICategoryService, CategoryService>();

        }

    }
}
