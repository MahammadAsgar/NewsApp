using Microsoft.EntityFrameworkCore;
using NewsMedia.Application.Context;
using NewsMedia.Domain.Models.Base;

namespace NewsMedia.Application.UnitOfWorks
{
    public class UnitofWork : IUnitofWork
    {
        readonly NewsDbContext _dbContext;
        public UnitofWork(NewsDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public void Commit()
        {
            var entities = _dbContext.ChangeTracker.Entries<BaseEntity>();
            foreach (var entityEntry in entities)
            {
                switch (entityEntry.State)
                {
                    case EntityState.Added:
                        entityEntry.Entity.AddDate = DateTime.Now;
                        entityEntry.Entity.IsActive = true;
                        break;
                    case EntityState.Modified:
                        entityEntry.Entity.UpdateDate = DateTime.Now;
                        break;
                    case EntityState.Deleted:
                        entityEntry.Entity.DeleteDate = DateTime.Now;
                        entityEntry.Entity.IsActive = false;
                        break;
                }
            }
            _dbContext.SaveChanges();
        }

        public async Task CommitAsync()
        {
            var entities = _dbContext.ChangeTracker.Entries<BaseEntity>();
            foreach (var entityEntry in entities)
            {
                switch (entityEntry.State)
                {
                    case EntityState.Added:
                        entityEntry.Entity.AddDate = DateTime.Now;
                        entityEntry.Entity.IsActive = true;
                        break;
                    case EntityState.Modified:
                        entityEntry.Entity.UpdateDate = DateTime.Now;
                        break;
                    case EntityState.Deleted:
                        entityEntry.Entity.DeleteDate = DateTime.Now;
                        entityEntry.Entity.IsActive = false;
                        break;
                }
            }
            await _dbContext.SaveChangesAsync();
        }
    }
}
