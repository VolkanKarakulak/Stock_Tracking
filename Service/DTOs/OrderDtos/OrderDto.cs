using Data.Entities;
using Service.DTOs.OrderIDetailDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.DTOs.OrderDtos
{
	public class OrderDto : BaseDto	
	{
		public DateTime OrderDate { get; set; }
		public string Status { get; set; }
		public bool IsPaid { get; set; }
		public DateTime? PaymentDate { get; set; }
		public string? PaymentMethod { get; set; }
		public decimal TotalAmount { get; set; }
		public string? TrackingNumber { get; set; }
		public List<OrderDetailDto> Items { get; set; } = new();

	}
}
