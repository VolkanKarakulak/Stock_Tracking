

using Data.Entities;
using Service.DTOs.ProductDtos;

namespace Service.DTOs.ProductStockDtos
{
    public class ProductStockDto : BaseDto
    {
        public int ProductId { get; set; }     
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public int? Quantity { get; set; }
        public DateTime LastUpdated { get; set; }
       
    }
}
