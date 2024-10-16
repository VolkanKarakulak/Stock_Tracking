using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.DTOs.ResponseDto
{
    public class PagedResponseDto<TDto> where TDto : class
    {
        public int? Id { get; set; }
        public TDto PagedDto { get; set; }
        public int PageNumber { get; set; }
        public int TotalPages { get; set; }
        public int TotalCount { get; set; }
    }
}
