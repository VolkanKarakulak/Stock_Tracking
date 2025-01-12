using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entities
{
	public class ProductSupplier 
	{
		public int SupplierId { get; set; }
		public int ProductId { get; set; }
		public Supplier? Supplier { get; set; }
		public Product? Product { get; set; }
	}
}
