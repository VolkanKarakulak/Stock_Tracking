using Data.Entities;
using Data.Repositories.GenericRepositories;
using Data.UnitOfWorks;
using Service.DTOs.PaginationDto;
using Service.DTOs.ProductDtos;
using Service.DTOs.ResponseDto;
using Service.Services.GenericService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services.ProductService
{
    public class ProductService : GenericService<Product>, IProductService
    {
        
       
        private readonly IGenericRepository<Product> _repository;
        private readonly IUnitOfWork _unitOfWork;


        public ProductService(IGenericRepository<Product> repository, IUnitOfWork unitOfWork) : base(repository, unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        public async Task<PagedResponseDto<IEnumerable<ProductDto>>> GetPagedCategoryAsync(PaginationDto paginationDto)
        {
            throw new NotImplementedException();
        }
    }
}
