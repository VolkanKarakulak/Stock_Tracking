using Data.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.DTOs.OrderDtos
{
    public class LastTenOrdersDto
    {
        public OrderStatus Status { get; set; }
        public decimal TotalAmount { get; set; }
        public string? TrackingNumber { get; set; }
        public string? PaymentMethod { get; set; }
        public int CustomerId { get; set; }

    }
}
