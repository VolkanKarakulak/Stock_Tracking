using Data.Enums;

namespace MVC.Models.OrderModels
{
    public class OrderUpdateModel
    {
        public int OrderId { get; set; }
        public bool IsApproved { get; set; }
        public bool IsCancelled { get; set; }
        public OrderStatus Status { get; set; }
        public bool IsPaid { get; set; }
        public string? CancellationReason { get; set; }

    }
}
