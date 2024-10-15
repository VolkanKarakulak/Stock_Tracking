using Data.Repositories.GenericRepositories;
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
    public static class DataRepositoryExtensions
    {
        public static void AddRepositoryServices(this IServiceCollection services)
        {
            // Repositories
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

            // UnitOfWorks
            services.AddScoped<IUnitOfWork, UnitOfWork>();
        }
    }
}
