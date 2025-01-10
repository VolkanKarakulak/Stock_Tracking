using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.DTOs.SupplierDtos
{
	public class SupplierAddDto
	{
		public string? Description { get; set; }
		public bool IsDeleted { get; set; }
		public bool IsActive { get; set; }
		public string Name { get; set; } = default!;
		public string PhoneNumber { get; set; } = default!;
	}
}
