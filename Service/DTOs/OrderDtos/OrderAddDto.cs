using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Service.DTOs.OrderItemDtos;

namespace Service.DTOs.OrderDtos
{
    public class OrderAddDto
	{
		public int CustomerId { get; set; }
		public List<OrderItemAddDTO> Items { get; set; }
		public string PaymentMethod { get; set; }
		public string Address { get; set; }
	}
}
