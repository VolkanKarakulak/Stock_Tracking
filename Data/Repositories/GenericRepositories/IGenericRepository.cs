
using System.Linq.Expressions;


namespace Data.Repositories.GenericRepositories
{
    public interface IGenericRepository<T> where T : class
    {
        Task<T> GetByIdAsync(int id);
        IQueryable<T> GetAll();
        IQueryable<T> Where(Expression<Func<T, bool>> expression); // where ile veritabanına yapışacak olan sorgu oluşturuluyor, sorgu yapılmıyor
        Task<bool> AnyAsync(Expression<Func<T, bool>> expression);
        Task CreateRangeAsync(IEnumerable<T> entities); // birden fazla kayıt
        Task CreateAsync(T entity);
        void Update(T entity); //void de olabilir, çünkü update/delete uzun süren işlemler değil
        void Delete(T entity); // void de olabilir
        void DeleteRange(IEnumerable<T> entities);
    }
}
