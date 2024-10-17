using Service.Exceptions.NotFoundExeptions;
using Microsoft.AspNetCore.Diagnostics;
using Service.DTOs.ResponseDto;
using Service.Exceptions;
using System.Text.Json;
using Service.DTOs.ResponseDtos;

namespace Web.API.MiddlewareHandlers
{
    public static class UseGlobalExceptionHandler
    {
        public static void ConfigureExceptionHandling(this IApplicationBuilder app)
        {
            app.UseExceptionHandler(x =>
            {
                x.Run(async context =>
                {
                    context.Response.ContentType = "application/json";
                    var exceptionFeature = context.Features.Get<IExceptionHandlerFeature>();
                    var statusCode = exceptionFeature!.Error switch
                    {
                        BadRequestException => 400,
                        PageNotFoundException => 404,
                        DataNotFoundException => 404,    
                        DataCreateFailedException => 400,
                        EmailOrPasswordWrongException => 400,
                        //ClientSideException => 400,
                        //EmailTakenException => 400,
                        _ => 500
                    };
                    context.Response.StatusCode = statusCode;

                    var response = CustomResponseDto<EmptyContentDto>.Failed(statusCode, exceptionFeature.Error.Message);

                    var jsonResponse = JsonSerializer.Serialize(response);
                    await context.Response.WriteAsync(jsonResponse);
                });
            });
        }
    }
}
