using Data.Entities;
using Data.Repositories.GenericRepositories;
using Data.Repositories.ProductStockRepositories;
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
        private readonly IProductStockRepository _productStockRepository;
        public ProductStockService(IProductStockRepository repository, IUnitOfWork unitOfWork) : base(repository, unitOfWork)
        {
            _productStockRepository = repository; // Özel repository'i kullanıyoruz
        }
        public override async Task<ProductStock?> CreateAsync(ProductStock entity)
        {
            // Stok ekleme işlemini repository üzerinden yapıyoruz
            return await _productStockRepository.CreateAsync(entity);
        }
    }
}
