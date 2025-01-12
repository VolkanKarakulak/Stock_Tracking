using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.DTOs.ProductDtos
{
    public class ProductAddDto 
    {
        public string Name { get; set; }
        public int Stock { get; set; }
        public decimal Price { get; set; }
        public IEnumerable<int> CategoryIds { get; set; }
        public string? Color { get; set; }
        public string? Material { get; set; }
        public string? Description { get; set; }
		public int? SupplierId { get; set; }


	}
}
