using Data.Entities;
using Service.Services.GenericService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services.ProductStockService
{
    public interface IProductStockService : IGenericService<ProductStock>
    {
    }
}
