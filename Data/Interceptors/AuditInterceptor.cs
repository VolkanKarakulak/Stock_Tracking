using Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Interceptors
{
    public class AuditInterceptor : SaveChangesInterceptor
    {
        private static readonly Dictionary<EntityState, IEntityStateBehavior> Behaviors = new()
        {
            { EntityState.Added, new AddedBehavior() },
            { EntityState.Modified, new ModifiedBehavior() }
        };

        public override ValueTask<InterceptionResult<int>> SavingChangesAsync(
            DbContextEventData eventData,
            InterceptionResult<int> result,
            CancellationToken cancellationToken = default)
        {
            var context = eventData.Context;

            foreach (var entry in context.ChangeTracker.Entries().ToList())
            {
                if (entry.Entity is not BaseEntity entity) continue;

                if (Behaviors.TryGetValue(entry.State, out var behavior))
                {
                    behavior.ApplyBehavior(context, entity);
                }
            }

            return base.SavingChangesAsync(eventData, result, cancellationToken);
        }
    }
}
