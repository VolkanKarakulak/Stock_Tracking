using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.DTOs.ResponseDtos
{
    public class ResponseDto
    {
        public int StatusCode { get; set; }
        public object Data { get; set; }
        public string Message { get; set; }
        public bool IsSuccess { get; set; }
        public bool IsShow { get; set; } = true;

    }
}
