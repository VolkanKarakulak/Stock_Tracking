using Data.Enums;

namespace MVC.Models.OrderModels
{
    public class LastTenOrdersModel
    {
        public OrderStatus Status { get; set; }
        public decimal TotalAmount { get; set; }
        public string? TrackingNumber { get; set; }
        public string? PaymentMethod { get; set; }
        public int CustomerId { get; set; }
    }
}
