using Data.Entities;
using Service.DTOs.TaxSettingDtos;
using Service.Services.GenericService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services.TaxSettingService
{
	public interface ITaxSettingService : IGenericService<TaxSetting, TaxSettingDto>
	{
		Task<decimal> GetTaxRateAsync();
	}
}
