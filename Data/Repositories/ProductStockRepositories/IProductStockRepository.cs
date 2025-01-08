using Data.Entities;
using Data.Repositories.GenericRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories.ProductStockRepositories
{
    public interface IProductStockRepository : IGenericRepository<ProductStock>
    {
        Task<ProductStock?> StateChangeAsync(ProductStock entity);
        Task<ProductStock> GetByColumnAsync(Expression<Func<ProductStock, bool>> predicate);

	}
}
