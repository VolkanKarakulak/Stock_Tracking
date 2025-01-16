using Data.Contexts;
using Data.Entities;
using Data.Repositories.GenericRepositories;
using Data.Repositories.ProductRepositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories.SupplierRepositories
{
	
	public class SupplierRepository : ISupplierRepository
	{
		private readonly IGenericRepository<Supplier> _repository;
		private readonly Stock_TrackingDbContext _context;
		protected readonly DbSet<Supplier> _dbSet;

		public SupplierRepository(IGenericRepository<Supplier> repository, Stock_TrackingDbContext context)
		{
			_repository = repository;
			_context = context;
			_dbSet = _context.Set<Supplier>();
		}

		public async Task<bool> AnyAsync(Expression<Func<Supplier, bool>> expression)
		{
			return await _repository.AnyAsync(expression);
		}

		public async Task<Supplier?> CreateAsync(Supplier entity, List<int> productIds)
		{
			await _repository.CreateAsync(entity);
			await _context.SaveChangesAsync();

			var productSuppliers = productIds.Select(productId => new ProductSupplier
			{
				SupplierId = entity.Id,
				ProductId = productId
			}).ToList();

			await _context.ProductSuppliers.AddRangeAsync(productSuppliers);

			await _context.SaveChangesAsync();
			entity.ProductSuppliers = productSuppliers;

			return entity;
		}

		public async Task<Supplier?> UpdateAsync(Supplier entity, List<int> productIds)
		{
			await _repository.UpdateAsync(entity);
		
			var existingSupplier = await _context.Suppliers
				.Include(s => s.ProductSuppliers)
				.FirstOrDefaultAsync(s => s.Id == entity.Id);

			// Mevcut productId listesi
			var existingProductIds = existingSupplier.ProductSuppliers
				.Select(c => c.ProductId)
				.ToList();

			// Silinecek ProductSupplier ilişkileri
			var productIdsToRemove = existingProductIds
				.Except(productIds)
				.ToList();

			// Yeni eklenecek ProductSupplier ilişkileri
			var productIdsToAdd = productIds
				.Except(existingProductIds)
				.ToList();

			// Silinecek ilişkileri veritabanından kaldır
			if (productIdsToRemove.Any())
			{
				var productSuppliersToRemove = existingSupplier.ProductSuppliers
					.Where(c => productIdsToRemove
					.Contains(c.ProductId))
					.ToList();

				_context.ProductSuppliers.RemoveRange(productSuppliersToRemove);
			}

			// Eklenecek yeni ilişkileri oluştur ve ekle
			if (productIdsToAdd.Any())
			{
				var productSuppliersToAdd = productIdsToAdd
					.Select(productId => new ProductSupplier
					{
						SupplierId = entity.Id,
						ProductId = productId
					})
					.ToList();

				await _context.ProductSuppliers.AddRangeAsync(productSuppliersToAdd);
			}

			await _context.SaveChangesAsync();

			// Güncellenmiş Supplier'ı döndür
			return existingSupplier;
		}

		public async Task<IQueryable<Supplier>> GetAllWitProductAsync()
		{
			var supplierList = await _context.Suppliers
				.Include(p => p.ProductSuppliers)
				.ToListAsync();

			return await Task.FromResult(supplierList.AsQueryable());
		}

		public async Task<bool> DeleteAsync(int id)
		{
			return await _repository.DeleteAsync(id);
		}

		public async Task<IQueryable<Supplier>> GetAllAsync()
		{
			return await _repository.GetAllAsync();
		}

		public IQueryable<Supplier> GetBy(Expression<Func<Supplier, bool>> expression)
		{
			return _repository.GetBy(expression);
		}

		public async Task<Supplier> GetByIdAsync(int id)
		{
			return await _repository.GetByIdAsync(id);
		}

		public async Task<(int, int, IQueryable<Supplier>)> GetPagedAsync(int pageNumber, int pageSize)
		{
			return await _repository.GetPagedAsync(pageNumber, pageSize);
		}

		public async Task<bool> IsEntityUpdateableAsync(int id)
		{
			return await _repository.IsEntityUpdateableAsync(id);
		}

		public Task<Supplier> UpdateAsync(Supplier entity)
		{
			throw new NotImplementedException();
		}

		public Task<Supplier?> CreateAsync(Supplier entity)
		{
			throw new NotImplementedException();
		}

		
	}
}
