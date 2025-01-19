using Data.Entities;
using Data.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.DTOs.OrderDtos
{
    public class OrderUpdateDto
    {
        public int OrderId { get; set; }
        public bool IsApproved { get; set; }
        public bool IsCancelled { get; set; }
        public OrderStatus Status { get; set; }
        public bool IsPaid { get; set; }
        public string? CancellationReason { get; set; }

    }
}
