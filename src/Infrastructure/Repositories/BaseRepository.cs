namespace Infrastructure.Repositories
{
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using Domain.Abstractions;
    using Microsoft.EntityFrameworkCore;

    public abstract class BaseRepository<T>(ApplicationDbContext dbContext) : IBaseRepository<T>
        where T : Entity
    {
        protected ApplicationDbContext DbContext { get; } = dbContext;

        public async Task<T?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            return await this.DbContext.Set<T>().FindAsync([id], cancellationToken);
        }

        public async Task AddAsync(T entity, CancellationToken cancellationToken = default)
        {
            await this.DbContext.AddAsync(entity, cancellationToken);
        }

        public async Task AddRangeAsync(IEnumerable<T> entity, CancellationToken cancellationToken = default)
        {
            await this.DbContext.AddRangeAsync(entity, cancellationToken);
        }

        public void Update(T entity)
        {
            this.DbContext.Update(entity);
        }

        public async Task<IEnumerable<T>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await this.DbContext.Set<T>().ToListAsync(cancellationToken);
        }

        public async Task<T?> FindOneAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default)
        {
            return await this.DbContext.Set<T>().FirstOrDefaultAsync(predicate, cancellationToken);
        }

        public void Remove(T entity)
        {
            this.DbContext.Remove(entity);
        }
    }
}
