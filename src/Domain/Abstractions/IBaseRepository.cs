namespace Domain.Abstractions
{
    using System.Linq.Expressions;

    public interface IBaseRepository<T>
    {
        Task<T?> GetByIdAsync(int id, CancellationToken cancellationToken = default);

        Task<T?> FindOneAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default);

        Task<IEnumerable<T>> GetAllAsync(CancellationToken cancellationToken = default);

        Task AddRangeAsync(IEnumerable<T> entity, CancellationToken cancellationToken = default);

        Task AddAsync(T entity, CancellationToken cancellationToken = default);

        void Remove(T entity);
    }
}
