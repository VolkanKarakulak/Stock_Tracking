using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entities
{
    public class Order : BaseEntity
    {
        public DateTime OrderDate { get; set; }
        public int CustomerId { get; set; }
        public Customer? Customer { get; set; } 
		public bool IsApproved { get; set; }
		public bool IsCancelled { get; set; }
		public string Status { get; set; } = "Pending"; // Varsayılan durum.
		public bool IsPaid { get; set; }
		public DateTime? PaymentDate { get; set; }
		public string? PaymentMethod { get; set; }
		public string? CancellationReason { get; set; }
		public DateTime? CancellationDate { get; set; }
		public decimal TotalAmount { get; set; }
		public decimal TaxAmount { get; set; } // vergi miktarı
		public int? StaffId { get; set; }
		public string? TrackingNumber { get; set; }
		public ICollection<OrderDetail>? OrderItems { get; set; }
	}
}
