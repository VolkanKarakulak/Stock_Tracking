using Data.Contexts;
using Data.Repositories.GenericRepositories;
using Data.UnitOfWorks;
using Microsoft.EntityFrameworkCore;
using Service.Abstract;
using Service.Concrete;
using Service.Mapping;
using System.Reflection;

namespace Web.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
            builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            builder.Services.AddScoped(typeof(IGenericService<>), typeof(GenericService<>));
            builder.Services.AddAutoMapper(typeof(MapProfile));

            builder.Services.AddDbContext<Stock_TrackingDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("SqlConnection"), sqlOptions =>
                {
                    sqlOptions.MigrationsAssembly(typeof(Stock_TrackingDbContext).Assembly.GetName().Name);
                });
            });

            var app = builder.Build();

                // Configure the HTTP request pipeline.
                if (app.Environment.IsDevelopment())
                {
                    app.UseSwagger();
                    app.UseSwaggerUI();
                }

                app.UseHttpsRedirection();

                app.UseAuthorization();


                app.MapControllers();

                app.Run();
        }
    }
}
