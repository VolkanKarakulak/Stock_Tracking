using AutoMapper;
using Data.Entities;
using Data.Repositories.CategoryRepositories;
using Data.Repositories.GenericRepositories;
using Data.Repositories.TaxSettingRepositories;
using Data.UnitOfWorks;
using Microsoft.EntityFrameworkCore;
using Service.DTOs.TaxSettingDtos;
using Service.Services.GenericService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services.TaxSettingService
{
	public class TaxSettingService : GenericService<TaxSetting, TaxSettingDto>, ITaxSettingService
	{
		private readonly IMapper _mapper;
		private readonly ITaxSettingRepository _taxSettingRepository;

		public TaxSettingService(IGenericRepository<TaxSetting> repository, IUnitOfWork unitOfWork, IMapper mapper, ITaxSettingRepository taxSettingRepository) : base(repository, unitOfWork, mapper)
		{
			_mapper = mapper;
			_taxSettingRepository = taxSettingRepository;
		}

		public async Task<decimal> GetTaxRateAsync()
		{
			return await _taxSettingRepository.GetTaxRateAsync();
		}
			
	}
}
