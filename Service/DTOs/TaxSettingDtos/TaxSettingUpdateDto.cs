using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.DTOs.TaxSettingDtos
{
	public class TaxSettingUpdateDto
	{
		public int Id { get; set; }
		public string? Description { get; set; }
		public bool IsDeleted { get; set; }
		public bool IsActive { get; set; }
		public decimal TaxRate { get; set; }
	}
}
