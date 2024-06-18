using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Data.Abstract
{
    public interface IGenericRepository<T> where T : class
    {
        Task<T> GetByIdAsync(int id);
        IQueryable<T> GetAll();
        IQueryable<T> Where(Expression<Func<T, bool>> expression); // where ile veritabanına yapışacak olan sorgu oluşturuluyor, sorgu yapılmıyor
        Task<bool> AnyAsync(Expression<Func<T, bool>> expression);
        Task CreateRangeAsync(IEnumerable<T> entities); // birden fazla kayıt, 
        Task CreateAsync(T entity);
        void Update(T entity); //void de olabilir, çünkü update/delete uzun süren işlemler değil
        void Delete(T entity); // void de olabilir
        void DeleteRange(IEnumerable<T> entities);
    }
}
