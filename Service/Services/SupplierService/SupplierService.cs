using AutoMapper;
using Data.Entities;
using Data.Repositories.CategoryRepositories;
using Data.Repositories.GenericRepositories;
using Data.Repositories.ProductRepositories;
using Data.Repositories.SupplierRepositories;
using Data.UnitOfWorks;
using Microsoft.EntityFrameworkCore;
using Service.DTOs.CategoryDtos;
using Service.DTOs.SupplierDtos;
using Service.Exceptions;
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
		private readonly IUnitOfWork _unitOfWork;
		private readonly ISupplierRepository _supplierRepository;
		private readonly IProductRepository _productRepository;
		public SupplierService(IGenericRepository<Supplier> repository, IUnitOfWork unitOfWork, IMapper mapper, ISupplierRepository supplierRepository, IProductRepository productRepository) : base(repository, unitOfWork, mapper)
		{
			_supplierRepository = supplierRepository;
			_mapper = mapper;
			_unitOfWork = unitOfWork;
			_productRepository = productRepository;
		}

		public async Task<SupplierDto> CreateSupplierAsync(SupplierAddDto dto)
		{
			var supplier = _mapper.Map<Supplier>(dto);
			if (supplier == null)
			{
				throw new DataCreateFailedException();
			}

			var productIds = dto.ProductIds!.ToList();
			var validProducts = await _productRepository
				.GetBy(x => productIds.Contains(x.Id) && !x.IsDeleted && x.IsActive)
				.AsNoTracking()
				.ToListAsync();

			if (validProducts.Count != productIds.Count)
			{
				throw new BadRequestException();
			}

			// Geçerli ürünleri ProductSupplier nesnelerine dönüştürüyoruz

			var productSuppliers = validProducts.Select(product => new ProductSupplier
			{
				ProductId = product.Id,
				SupplierId = supplier.Id,  
								 
			}).ToList();

			// ProductSuppliers koleksiyonunu supplier'a ekliyoruz
			supplier.ProductSuppliers = productSuppliers;

			var createdSupplier = await _supplierRepository.CreateAsync(supplier);

			await _unitOfWork.CommitAsync();

			return _mapper.Map<SupplierDto>(createdSupplier);
		}
	}
}
