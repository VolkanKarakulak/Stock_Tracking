﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entities
{
	public class TaxSetting : BaseEntity
	{
		public decimal TaxRate { get; set; }
		//public string? Category { get; set; }
	}
}
