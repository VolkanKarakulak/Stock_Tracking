using Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.EntityHelper
{
    public class EntityHelper<T> where T : BaseEntity
    {
        private readonly DbContext _context;
        private readonly DbSet<T> _dbSet;    

        public EntityHelper(DbContext context)
        {           
            _context = context;
            _dbSet = _context.Set<T>();
        }

        public T? GetOldEntity(int id)
        {
            return _dbSet.AsNoTracking().FirstOrDefault(x => x.Id == id);
        }

        public void UpdateEntityProperties(T oldEntity, T newEntity)
        {
            var properties = typeof(T).GetProperties();

            foreach (var property in properties)
            {
                if (property.CanWrite && property.CanRead)
                {
                    var newValue = property.GetValue(newEntity);
                    var oldValue = property.GetValue(oldEntity);

                    if (newValue != null && !newValue.Equals(oldValue))
                    {
                        _context.Entry(oldEntity).Property(property.Name).IsModified = true;
                        _context.Entry(oldEntity).Property(property.Name).CurrentValue = newValue;
                    }
                }
            }
        }
    }
}
