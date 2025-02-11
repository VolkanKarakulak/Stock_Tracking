﻿using Data.Entities;
using Data.Repositories.CategoryRepositories;
using Data.Repositories.GenericRepositories;
using Data.Repositories.OrderRepositories;
using Data.Repositories.ProductRepositories;
using Data.Repositories.ProductStockRepositories;
using Data.Repositories.SupplierRepositories;
using Data.Repositories.TaxSettingRepositories;
using Data.Repositories.UserRepositories;
using Data.UnitOfWorks;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Data.Extensions
{
    public static class RepositoryExtensions
    {
        public static void AddRepositoryExtensions(this IServiceCollection services)
        {
            // Repositories
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

            // UnitOfWorks
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<IProductStockRepository, ProductStockRepository>();
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<ITaxSettingRepository, TaxSettingRepository>();
            services.AddScoped<ISupplierRepository, SupplierRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
        }
    }
}
