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
        void ApplyBehavior(DbContext context, BaseEntity entity);
    }

    // AddedBehavior sınıfı, IEntityStateBehavior'u implement eder
    public class AddedBehavior : IEntityStateBehavior
    {
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
            entity.UpdatedDate = DateTime.Now;
            context.Entry(entity).Property(x => x.CreatedDate).IsModified = false;
        }
    }
}

