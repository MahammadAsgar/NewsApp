using Microsoft.EntityFrameworkCore;
using NewsMedia.Application.Context;
using NewsMedia.Application.Repositories.Abstractions;
using NewsMedia.Domain.Models.Base;
using System.Linq.Expressions;

namespace NewsMedia.Application.Repositories.Implementations
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        readonly DbSet<T> _dbSet;
        readonly NewsDbContext _dbContext;
        public GenericRepository(NewsDbContext dbContext)
        {
            _dbSet = dbContext.Set<T>();
            _dbContext = dbContext;
        }
        public async Task AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
        }

        public async Task AddRangeAsync(IEnumerable<T> entities)
        {
            await _dbSet.AddRangeAsync(entities);
        }
        public async Task<T> GetByIdAsync(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task<IEnumerable<T>> GetEntities()
        {
            return await _dbSet.OrderBy(x => x.Id).ToListAsync();
        }

        public void Remove(T entity)
        {
            _dbSet.Remove(entity);
        }

        public T Update(T entity)
        {
            _dbSet.Update(entity);
            return entity;
        }

        public IQueryable<T> Where(Expression<Func<T, bool>> predicate)
        {
            return _dbSet.OrderBy(x => x.Id).Where(predicate);
        }
    }
}
