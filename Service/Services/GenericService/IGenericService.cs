using Service.DTOs.PaginationDto;
using Service.DTOs.ResponseDto;
using System.Linq.Expressions;

namespace Service.Services.GenericService
{
    public interface IGenericService<T> where T : class
    {
        Task<T> GetByIdAsync(int id);
        Task<IEnumerable<T>> GetAllAsync(); // Task<IEnumerable<T>> GetAllAsync(); de olabilir tüm datayı çeker
        IQueryable<T> GetBy(Expression<Func<T, bool>> expression); // where ile veritabanına yapılacak olan sorgu oluşturuluyor, sorgu yapılmıyor
        Task<bool> AnyAsync(Expression<Func<T, bool>> expression);
        Task<IEnumerable<T>> CreateRangeAsync(IEnumerable<T> entities); // birden fazla kayıt, 
        Task<T> CreateAsync(T entity);
        Task UpdateAsync(T entity); //void de olabilir, çünkü update/delete uzun süren işlemler değil
        Task<bool> DeleteAsync(int id); // void de olabilir
        //Task DeleteRangeAsync(IEnumerable<int> entityIds);
        Task<PagedResponseDto<IEnumerable<T>>> GetPagedAsync(PaginationDto paginationDto);
    }
}
