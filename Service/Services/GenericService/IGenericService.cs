using Service.DTOs.PaginationDto;
using Service.DTOs.ResponseDto;
using System.Linq.Expressions;

namespace Service.Services.GenericService
{
    public interface IGenericService<TEntity, TDto>
        where TEntity : class
        where TDto : class
    {
        Task<TDto> GetByIdAsync(int id); 
        Task<IEnumerable<TDto>> GetAllAsync(); 
        IQueryable<TEntity> GetBy(Expression<Func<TEntity, bool>> expression); 
        Task<bool> AnyAsync(Expression<Func<TEntity, bool>> expression); 
        Task<IEnumerable<TDto>> CreateRangeAsync(IEnumerable<TDto> dtos); 
        Task<TDto> CreateAsync(TDto dto); 
        Task<TDto> UpdateAsync(TDto dto); 
        Task<bool> DeleteAsync(int id); 
        Task<PagedResponseDto<IEnumerable<TDto>>> GetPagedAsync(PaginationDto paginationDto); 
    }
}
