using Data.Contexts;
using Data.Repositories.GenericRepositories;
using Data.UnitOfWorks;
using Microsoft.EntityFrameworkCore;
using Service.Mapping;
using Service.Extensions;
using Data.Extensions;
using Web.API.MiddlewareHandlers;
using Web.API.Hubs;

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
            builder.Services.AddAutoMapper(typeof(MapProfile));
            builder.Services.AddServiceExtensions();
            builder.Services.AddRepositoryExtensions();
			builder.Services.AddSignalR();

			builder.Services.AddDbContext<Stock_TrackingDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("SqlConnection"), sqlOptions =>
                {
                    sqlOptions.MigrationsAssembly(typeof(Stock_TrackingDbContext).Assembly.GetName().Name);
                });

            });

			builder.Services.AddCors(options =>
			{
				options.AddPolicy("AllowAllOrigins", builder =>
				{
                      builder
                             .AllowAnyOrigin()  // Bu, tüm kökenlerden gelen isteklere izin verir
					         .AllowAnyMethod()
					         .AllowAnyHeader();
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
                app.ConfigureExceptionHandling();
                app.UseAuthorization();
			    app.MapHub<OrderHub>("/orderHub");
			    app.UseCors("AllowAllOrigins");

			app.MapControllers();

                app.Run();
        }
    }
}
