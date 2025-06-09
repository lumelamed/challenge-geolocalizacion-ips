namespace Domain.Interfaces
{
    using Domain.Abstractions;
    using Domain.Entities;

    public interface ICountryRepository : IBaseRepository<Country>
    {
        Task<Country?> GetByCodeAsync(string code, CancellationToken cancellationToken = default);
    }
}
