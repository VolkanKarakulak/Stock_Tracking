using System.Diagnostics.CodeAnalysis;

namespace MVC.Models.ResponseModels
{
    public class ResponseModel<T>
    {
        public int StatusCode { get; set; }

        [AllowNull]
        public T Data { get; set; }

        [AllowNull]
        public string Message { get; set; }

        [AllowNull]
        public bool IsShow { get; set; }
    }
}
