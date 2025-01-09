using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Enums
{
	public enum OrderStatus : byte
	{
		Pending = 1,      // Beklemede
		Approved = 2,     // Onaylandı
		Cancelled = 3   // İptal Edildi

		//Shipped = 4,      // Kargolandı
		//Delivered = 5,    // Teslim Edildi
		//Returned = 6      // İade Edildi

	}
}
