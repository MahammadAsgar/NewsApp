using NewsMedia.Domain.Models.Base;
using System.Linq.Expressions;

namespace NewsMedia.Application.Repositories.Abstractions
{
    public interface IGenericRepository<T> where T : BaseEntity
    {
        Task<T> GetByIdAsync(int id);
        Task<IEnumerable<T>> GetEntities();
        IQueryable<T> Where(Expression<Func<T, bool>> predicate);
        Task AddAsync(T entity);
        Task AddRangeAsync(IEnumerable<T> entities);
        void Remove(T entity);
        T Update(T entity);
    }
}
