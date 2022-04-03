using Microsoft.EntityFrameworkCore;
using Orion.Core;
using Orion.Core.Base;
using System.Linq.Expressions;

namespace Orion.Infra.Data.Repositories
{
    public class BaseRepository<T> : IAsyncRepository<T> where T : Entity
    {
        private readonly ApplicationContext _context;
        private readonly DbSet<T> _dbset;

        public BaseRepository(ApplicationContext context)
        {
            _context = context;
            _dbset = _context.Set<T>();
        }

        public async Task AddAsync(T entity)
            => await _dbset.AddAsync(entity);

        public Task DeleteAsync(T entity)
        {
            _dbset.Remove(entity);
            return Task.CompletedTask;
        }

        public Task<IQueryable<T>> GetAllAsync(Expression<Func<T, bool>>? filter, Expression<Func<T, object>>[]? includes)
        {
            IQueryable<T> query = AddQueryAttributes(filter!, includes!);
            return Task.FromResult(query);
        }

        public Task<T> GetAsync(Expression<Func<T, bool>>? filter, Expression<Func<T, object>>[]? includes)
        {
            IQueryable<T> query = AddQueryAttributes(filter!, includes!);
            return Task.FromResult(query.FirstOrDefault()!);
        }

        public Task UpdateAsync(T entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            return Task.CompletedTask;
        }

        private IQueryable<T> AddQueryAttributes(Expression<Func<T, bool>> filter, Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = _dbset.AsQueryable();

            if (includes is not null)
                includes.ToList().ForEach(include =>
                {
                    if (include != null)
                        query = query.Include(include);
                });

            if (filter is not null)
                query = query.Where(filter);

            return query;
        }
    }
}
