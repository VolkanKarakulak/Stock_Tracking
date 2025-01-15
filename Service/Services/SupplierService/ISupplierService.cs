using Data.Entities;
using Service.DTOs.SupplierDtos;
using Service.Services.GenericService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services.SupplierService
{
	public interface ISupplierService : IGenericService<Supplier, SupplierDto>
	{
		Task<SupplierDto> CreateSupplierAsync(SupplierAddDto dto);
	}
}
