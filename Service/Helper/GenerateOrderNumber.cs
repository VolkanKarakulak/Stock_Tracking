using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Helper
{
	public class GenerateOrderNumber
	{
		public static string CreateOrderNumber()
		{
			// Günün tarihini alıyoruz (yyyyMMdd formatında).
			string datePart = DateTime.Now.ToString("yyyyMMdd");

			// Üç basamağa sahip rastgele bir sayı ekliyoruz.
			string orderPart = $"{datePart}-{new Random().Next(1, 1000):D3}";

			// Sipariş numarasını oluşturup döndürüyoruz.
			return $"ORD-{orderPart}";
		}
	}

}
