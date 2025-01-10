using AutoMapper;
using Data.Entities;
using Data.Repositories.CategoryRepositories;
using Data.Repositories.GenericRepositories;
using Data.Repositories.SupplierRepositories;
using Data.UnitOfWorks;
using Service.DTOs.CategoryDtos;
using Service.DTOs.SupplierDtos;
using Service.Services.CategoryService;
using Service.Services.GenericService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services.SupplierService
{
	public class SupplierService : GenericService<Supplier, SupplierDto>, ISupplierService
	{
		private readonly IMapper _mapper;
		private readonly ISupplierRepository _supplierRepository;
		public SupplierService(IGenericRepository<Supplier> repository, IUnitOfWork unitOfWork, IMapper mapper, ISupplierRepository supplierRepository) : base(repository, unitOfWork, mapper)
		{
			_supplierRepository = supplierRepository;
			_mapper = mapper;
		}
	}
}
