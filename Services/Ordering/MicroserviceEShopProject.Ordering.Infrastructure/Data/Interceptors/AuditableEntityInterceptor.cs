using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace MicroserviceEShopProject.Ordering.Infrastructure.Data.Interceptors
{
    public class AuditableEntityInterceptor : SaveChangesInterceptor
    {
        public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
        {
            UpdateEntites(eventData.Context);

            return base.SavingChanges(eventData, result);
        }

        public override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
        {
            UpdateEntites(eventData.Context);

            return base.SavingChangesAsync(eventData, result, cancellationToken);
        }


        public void UpdateEntites(DbContext? dbContext)
        {
            if (dbContext == null) return;

            foreach (var item in dbContext.ChangeTracker.Entries<IEntity>())
            {
                if (item.State == EntityState.Added)
                {
                    item.Entity.CreatedBy = "Mehmet";
                    item.Entity.CreatedAt = DateTime.UtcNow;
                }

                if (item.State == EntityState.Added || item.State == EntityState.Modified || item.HasChangedOwnedEntities())
                {
                    item.Entity.LastModifiedBy = "Mehmet";
                    item.Entity.LastModified = DateTime.UtcNow;
                }
            }
        }
    }
}

public static class Extensions
{
    public static bool HasChangedOwnedEntities(this EntityEntry entry) =>
        entry.References.Any(r => r.TargetEntry != null && r.TargetEntry.Metadata.IsOwned()
        && (r.TargetEntry.State == EntityState.Added || r.TargetEntry.State == EntityState.Modified));
}
