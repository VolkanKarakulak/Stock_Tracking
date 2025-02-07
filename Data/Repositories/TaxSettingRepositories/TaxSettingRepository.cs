using Data.Contexts;
using Data.Entities;
using Data.EntityHelper;
using Data.Interceptors;
using Data.Repositories.GenericRepositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories.TaxSettingRepositories
{
	public class TaxSettingRepository : ITaxSettingRepository
	{
		private readonly IGenericRepository<TaxSetting> _repository;
		private readonly Stock_TrackingDbContext _context;
		protected readonly DbSet<TaxSetting> _dbSet;

		public TaxSettingRepository(IGenericRepository<TaxSetting> repository, Stock_TrackingDbContext context)
		{
			_repository = repository;
			_context = context;
			_dbSet = _context.Set<TaxSetting>();
		}

		public async Task<bool> AnyAsync(Expression<Func<TaxSetting, bool>> expression)
		{
			return await _repository.AnyAsync(expression);
		}

		public async Task<TaxSetting?> CreateAsync(TaxSetting entity)
		{
			return await _repository.CreateAsync(entity);
		}

		public async Task<bool> DeleteAsync(int id)
		{
			return await _repository.DeleteAsync(id);
		}

		public async Task<IQueryable<TaxSetting>> GetAllAsync()
		{
			return await _repository.GetAllAsync();
		}

		public IQueryable<TaxSetting> GetBy(Expression<Func<TaxSetting, bool>> expression)
		{
			return _repository.GetBy(expression);
		}

		public async Task<TaxSetting> GetByIdAsync(int id)
		{
			return await _repository.GetByIdAsync(id);
		}

		public Task<(int, int, IQueryable<TaxSetting>)> GetPagedAsync(int pageNumber, int pageSize)
		{
			throw new NotImplementedException();
		}

		public async Task<bool> IsEntityUpdateableAsync(int id)
		{
			return await _repository.IsEntityUpdateableAsync(id);
		}

        public async Task<TaxSetting> UpdateAsync(TaxSetting entity)
        {
            var entry = _context.Entry(entity);

            if (entry.State == EntityState.Detached)
                return await HandleDetachedEntity(entity); // 🚀 Erken çıkış, gereksiz işlemleri engelle

            if (entry.State != EntityState.Modified)
                throw new InvalidOperationException("Entity state is not valid for an update operation."); // 🚀 Hatalı durumu hemen yakala

            entry.State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return entity;
        }

        // Detached durumda olan entity'yi işleyip güncelleyen yardımcı metod
        private async Task<TaxSetting> HandleDetachedEntity(TaxSetting entity)
        {
            var entityHelper = new EntityHelper<TaxSetting>(_context);
            var oldEntity = entityHelper.GetOldEntity(entity.Id);

            if (oldEntity == null)
                throw new InvalidOperationException("Entity with the specified ID does not exist in the context."); // 🚀 Hatalı durumu hemen yakala

            var behavior = new ModifiedBehavior();
            behavior.ApplyBehavior(_context, entity);

            entityHelper.UpdateEntityProperties(oldEntity, entity);
            await _context.SaveChangesAsync(); // Güncellenmiş eski varlığı kaydet

            return oldEntity; // Güncellenmiş varlık döndürülüyor
        }


        public async Task<decimal> GetTaxRateAsync()
		{
			var taxSetting = await _context.TaxSettings.FirstOrDefaultAsync(t => t.IsActive); // Örneğin, varsayılan vergi oranını al.
			if (taxSetting == null)
			{
				throw new Exception("Vergi oranı ayarı bulunamadı!");
			}
			var taxRate = taxSetting.TaxRate/100;
			return taxRate;
		}

		public async Task<decimal> CalculateTaxAmount(decimal basePrice)
		{
			var taxRate = await GetTaxRateAsync();
			return basePrice * taxRate;
		}
	}

}
