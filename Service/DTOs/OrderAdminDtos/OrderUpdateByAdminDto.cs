﻿using Data.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.DTOs.OrderAdminDtos
{
	public class OrderUpdateByAdminDto
	{
		public int OrderId { get; set; }
		public bool IsApproved { get; set; }
		public bool IsCancelled { get; set; }
		public string Status { get; set; } 
		public bool IsPaid { get; set; }
		public string? CancellationReason { get; set; }
		
	}
}
