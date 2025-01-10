using Data.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Service.DTOs.ProductDtos;

namespace Service.DTOs.SupplierDtos
{
	public class SupplierDto : BaseDto
	{
		public string? Description { get; set; }
		public string Name { get; set; } = default!;
		public string PhoneNumber { get; set; } = default!;
		public IEnumerable<ProductDto> ProductsSupplied { get; set; }
	}
}
