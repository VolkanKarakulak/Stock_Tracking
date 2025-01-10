using Data.Entities;
using Data.Repositories.GenericRepositories;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories.SupplierRepositories
{
	public interface ISupplierRepository : IGenericRepository<Supplier>
	{
		Task<IQueryable<Supplier>> GetAllWitProductAsync();
	}
}
