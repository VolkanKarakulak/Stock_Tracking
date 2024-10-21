
using System.Linq.Expressions;


namespace Data.Repositories.GenericRepositories
{
    public interface IGenericRepository<T> where T : class
    {
        Task<T> GetByIdAsync(int id);
        Task<IQueryable<T>> GetAllAsync();
        IQueryable<T> GetBy(Expression<Func<T, bool>> expression); // where ile veritabanına yapılacak olan sorgu oluşturuluyor, sorgu yapılmıyor
        Task<bool> AnyAsync(Expression<Func<T, bool>> expression);
        Task<IEnumerable<T>> CreateRangeAsync(IEnumerable<T> entities);
        Task<T?> CreateAsync(T entity);
        T Update(T entity); 
        Task<bool> IsEntityUpdateableAsync(int id);
        bool Delete(int id); 
        //bool DeleteRange(IEnumerable<int> entityIds);
        Task<(int, int, IQueryable<T>)> GetPagedAsync(int pageNumber, int pageSize);
    }
}
