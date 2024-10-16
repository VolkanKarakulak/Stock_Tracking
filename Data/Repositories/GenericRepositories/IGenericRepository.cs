
using System.Linq.Expressions;


namespace Data.Repositories.GenericRepositories
{
    public interface IGenericRepository<T> where T : class
    {
        Task<T> GetByIdAsync(int id);
        Task<IQueryable<T>> GetAllAsync();
        IQueryable<T> GetBy(Expression<Func<T, bool>> expression); // where ile veritabanına yapılacak olan sorgu oluşturuluyor, sorgu yapılmıyor
        Task<bool> AnyAsync(Expression<Func<T, bool>> expression);
        Task CreateRangeAsync(IEnumerable<T> entities); // birden fazla kayıt
        Task<T?> CreateAsync(T entity);
        T Update(T entity); //void de olabilir, çünkü update/delete uzun süren işlemler değil
        Task<bool> IsEntityUpdateableAsync(int id);
        bool Delete(int id); // void de olabilir
        bool DeleteRange(IEnumerable<T> entities);
        Task<(int, int, IQueryable<T>)> GetPagedAsync(int pageNumber, int pageSize);
    }
}
