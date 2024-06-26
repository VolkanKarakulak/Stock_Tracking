﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Service.Abstract
{
    public interface IGenericService<T> where T : class
    {
        Task<T> GetByIdAsync(int id);
        Task<IEnumerable<T>> GetAllAsync(); // Task<IEnumerable<T>> GetAllAsync(); de olabilir tüm datayı çeker
        IQueryable<T> Where(Expression<Func<T, bool>> expression); // where ile veritabanına yapışacak olan sorgu oluşturuluyor, sorgu yapılmıyor
        Task<bool> AnyAsync(Expression<Func<T, bool>> expression);
        Task<IEnumerable<T>> CreateRangeAsync(IEnumerable<T> entities); // birden fazla kayıt, 
        Task<T> CreateAsync(T entity);
        Task UpdateAsync(T entity); //void de olabilir, çünkü update/delete uzun süren işlemler değil
        Task DeleteAsync(T entity); // void de olabilir
        Task DeleteRangeAsync(IEnumerable<T> entities);
    }
}
