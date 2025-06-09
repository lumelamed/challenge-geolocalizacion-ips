namespace Domain.Abstractions
{
    using Domain.Interfaces;

    public interface IUnitOfWork
    {
        IIpInfoRepository IpInfoRepository { get; }

        ICountryRepository CountryRepository { get; }

        Task<int> SaveAsync(CancellationToken cancellationToken = default);
    }
}
