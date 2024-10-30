using Data.Entities;
using Data.Repositories.GenericRepositories;
using Data.UnitOfWorks;
using Service.Services.GenericService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services.ProductStockService
{
    public class ProductStockService : GenericService<ProductStock>, IProductStockService
    {
        public ProductStockService(IGenericRepository<ProductStock> repository, IUnitOfWork unitOfWork) : base(repository, unitOfWork)
        {
        }
    }
}
