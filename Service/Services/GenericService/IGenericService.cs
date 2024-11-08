using Service.DTOs.PaginationDto;
using Service.DTOs.ResponseDto;
using System.Linq.Expressions;

namespace Service.Services.GenericService
{
    public interface IGenericService<TEntity, TDto>
        where TEntity : class
        where TDto : class
    {
        Task<TDto> GetByIdAsync(int id); // Dönüş tipi DTO oldu
        Task<IEnumerable<TDto>> GetAllAsync(); // Tüm veriyi DTO listesi olarak döndürür
        IQueryable<TEntity> GetBy(Expression<Func<TEntity, bool>> expression); // Filtreleme için IQueryable olarak döndürür
        Task<bool> AnyAsync(Expression<Func<TEntity, bool>> expression); // Varlık mevcut mu kontrolü
        Task<IEnumerable<TDto>> CreateRangeAsync(IEnumerable<TDto> dtos); // Birden fazla DTO oluşturur
        Task<TDto> CreateAsync(TDto dto); // Tek bir DTO oluşturur
        Task<TDto> UpdateAsync(TDto dto); // DTO güncelleme
        Task<bool> DeleteAsync(int id); // Silme işlemi, başarılıysa true döner

        Task<PagedResponseDto<IEnumerable<TDto>>> GetPagedAsync(PaginationDto paginationDto); // Sayfalama desteği
    }
}
