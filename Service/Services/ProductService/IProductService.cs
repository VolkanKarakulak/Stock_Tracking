using Data.Entities;
using Service.DTOs.PaginationDto;
using Service.DTOs.ProductDtos;
using Service.DTOs.ResponseDto;
using Service.Services.GenericService;


namespace Service.Services.ProductService
{
    public interface IProductService : IGenericService<Product>
    {
        Task<PagedResponseDto<IEnumerable<ProductDto>>> GetPagedCategoryAsync(PaginationDto paginationDto);
    }
}
