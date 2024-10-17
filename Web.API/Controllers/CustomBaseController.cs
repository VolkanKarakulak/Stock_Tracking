using Microsoft.AspNetCore.Mvc;
using Service.DTOs.ResponseDtos;

namespace Web.API.Controllers
{
    public class ResponseBuilder
    {
        public static ResponseDto CreateResponse(Object data, bool isSuccess, string message, int statusCode = 204)
        {
            return new ResponseDto
            {
                Data = data,
                StatusCode = statusCode,
                Message = message,
                IsSuccess = isSuccess
            };
        }
    }
}
