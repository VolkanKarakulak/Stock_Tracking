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

		public Task<Supplier?> CreateAsync(Supplier entity)
		{
			throw new NotImplementedException();
		}

		public Task<IEnumerable<Supplier>> CreateRangeAsync(IEnumerable<Supplier> entities)
		{
			throw new NotImplementedException();
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

		public Task<(int, int, IQueryable<Supplier>)> GetPagedAsync(int pageNumber, int pageSize)
		{
			throw new NotImplementedException();
		}

		public async Task<bool> IsEntityUpdateableAsync(int id)
		{
			return await _repository.IsEntityUpdateableAsync(id);
		}

		public Task<Supplier> UpdateAsync(Supplier entity)
		{
			throw new NotImplementedException();
		}

		public async Task<IQueryable<Supplier>> GetAllWitProductAsync()
		{
			var supplierList = await _context.Suppliers
				.Include(p => p.ProductSuppliers)
				.ToListAsync();

			return await Task.FromResult(supplierList.AsQueryable());
		}
	}
}
