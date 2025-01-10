using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.DTOs.BasketDtos
{
	public class BasketInfoDto // Redis ile kullanılacak
	{
		public int CustomerId { get; set; } 
		public DateTime DateOfAddition { get; set; } = DateTime.UtcNow;
		public IEnumerable<BasketItemInfoDto> Items { get; set; } = new List<BasketItemInfoDto>(); // Sepetteki ürünler
	}
}
