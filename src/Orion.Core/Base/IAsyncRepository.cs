using System.Linq.Expressions;

namespace Orion.Core.Base
{
    public interface IAsyncRepository<T> where T : Entity
    {
        Task<IQueryable<T>> GetAllAsync(Expression<Func<T, bool>>? filter = null, Expression<Func<T, object>>[]? includes = null);
        Task<T> GetAsync(Expression<Func<T, bool>>? filter = null, Expression<Func<T, object>>[]? includes = null);
        Task AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(T entity);
    }
}
