using Azure.Core;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Service.DTOs.ResponseDtos
{
    public class CustomResponseDto<T>
    {
        public object Data { get; set; }

        [JsonIgnore]
        public int StatusCode { get; set; }
        public string Message { get; set; } 
        public List<string> Errors { get; set; }


        public static CustomResponseDto<T> Success(int statusCode, Object data, string message = null)
        {
            return new CustomResponseDto<T> { Data = data, StatusCode = statusCode, Message = message, Errors = null };
        }

        // Başarı durumu (data yoksa)
        public static CustomResponseDto<T> Success(int statusCode, string message = null)
        {
            return new CustomResponseDto<T> { StatusCode = statusCode, Message = message, Data = default, Errors = null };
        }

        // Hata durumu (birden fazla hata)
        public static CustomResponseDto<T> Failed(int statusCode, List<string> errors, string message = null)
        {
            return new CustomResponseDto<T> { StatusCode = statusCode, Errors = errors, Message = message, Data = default };
        }

        // Hata durumu (tek hata)
        public static CustomResponseDto<T> Failed(int statusCode, string error, string message = null)
        {
            return new CustomResponseDto<T> { StatusCode = statusCode, Errors = new List<string> { error }, Message = message, Data = default };
        }








    }
}
