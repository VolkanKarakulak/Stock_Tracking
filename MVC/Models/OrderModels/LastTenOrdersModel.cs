using Data.Enums;

namespace MVC.Models.OrderModels
{
    public class LastTenOrdersModel
    {
        public OrderStatus Status { get; set; }
        public decimal TotalAmount { get; set; }
        public string? TrackingNumber { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CustomerName { get; set; }
    }
}
