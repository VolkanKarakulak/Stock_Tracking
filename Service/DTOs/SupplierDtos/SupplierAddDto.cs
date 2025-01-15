using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.DTOs.SupplierDtos
{
	public class SupplierAddDto
	{
		public string? Name { get; set; } 
		public string? PhoneNumber { get; set; } 
		public string? Description { get; set; }
		public bool IsDeleted { get; set; }
		public bool IsActive { get; set; }
		public IEnumerable<int>? ProductIds { get; set; } 

	}
}
