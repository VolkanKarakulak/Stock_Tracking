using Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Interceptors
{
    // IEntityStateBehavior interface tanımı
    public interface IEntityStateBehavior
    {
        void ApplyBehavior(DbContext context, BaseEntity entities);
    }
    public interface IEntityStateBehavior<T> where T : class
    {
        void ApplyBehavior(DbContext context, T entity);
    }

    // AddedBehavior sınıfı, IEntityStateBehavior'u implement eder
    public class AddedBehavior : IEntityStateBehavior
    {
        public void ApplyBehavior(DbContext context, IEnumerable<BaseEntity> entity)
        {
            foreach(var item in entity)
            {
                item.CreatedDate = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd HH:mm"));
                item.IsActive = true;
                context.Entry(item).Property(x => x.UpdatedDate).IsModified = false;
            }
           
        }
        public void ApplyBehavior(DbContext context, BaseEntity entity)
        {
            
            entity.CreatedDate = DateTime.Now;
            entity.IsActive = true;
            context.Entry(entity).Property(x => x.UpdatedDate).IsModified = false;
        }
    }

    // ModifiedBehavior sınıfı, IEntityStateBehavior'u implement eder
    public class ModifiedBehavior : IEntityStateBehavior
    {
        public void ApplyBehavior(DbContext context, BaseEntity entity)
        {
            entity.UpdatedDate = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd HH:mm"));
            context.Entry(entity).Property(x => x.CreatedDate).IsModified = false;

        }
        public class UpdatedBehavior : IEntityStateBehavior<ProductStock>
        {
            public void ApplyBehavior(DbContext context, ProductStock entity)
            {
                entity.UpdatedDate = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd HH:mm"));                
            }
        }
    }
}

