

using Data.Entities;
using Service.DTOs.ProductDtos;

namespace Service.DTOs.CategoryDtos
{
    public class CategoryDto : BaseDto
    {
        public string? Name { get; set; }
        public string? Description { get; set; }

        //public IEnumerable<ProductDto>? Products { get; set; }
    }
}
