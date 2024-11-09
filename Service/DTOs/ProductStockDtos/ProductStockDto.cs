

using Data.Entities;
using Service.DTOs.ProductDtos;

namespace Service.DTOs.ProductStockDtos
{
    public class ProductStockDto 
    {
        public int Id { get; set; }
        public int ProductId { get; set; }     
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public int? Quantity { get; set; }
        public DateTime UpdatedDate { get; set; }
        public DateTime CreatedDate { get; set; }
       
    }
}
