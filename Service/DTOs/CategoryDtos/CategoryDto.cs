

using Data.Entities;
using Service.DTOs.ProductDtos;

namespace Service.DTOs.CategoryDtos
{
    public class CategoryDto
    {
        public string Name { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public string Description { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public IEnumerable<ProductDto>? Products { get; set; }
    }
}
