using Data.Enums;
using Service.DTOs.OrderIDetailDtos;

namespace MVC.Models.OrderModels
{
    public class OrderModel
    {
        public int Id { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsActive { get; set; }
        public DateTime OrderDate { get; set; }
        public OrderStatus Status { get; set; }
        public bool IsPaid { get; set; }
        public DateTime? PaymentDate { get; set; }
        public string? PaymentMethod { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal TaxAmount { get; set; }
        public string? TrackingNumber { get; set; }
        public IEnumerable<OrderDetailDto> Items { get; set; }

    }
}
