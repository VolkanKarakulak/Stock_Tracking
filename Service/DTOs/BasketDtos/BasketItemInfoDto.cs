using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.DTOs.BasketDtos
{
	public class BasketItemInfoDto // Redis ile kullanılacak
	{
		public int ProductId { get; set; } // Ürün kimliği
		public int Quantity { get; set; } 
		public decimal UnitPrice { get; set; } // Ürün birim fiyatı
		public decimal TotalPrice => Quantity * UnitPrice; // Toplam fiyat (hesaplanan özellik)
	}
}
