namespace Domain.Interfaces
{
    using Domain.Abstractions;
    using Domain.DTOs;
    using Domain.Entities;

    public interface IIpInfoRepository : IBaseRepository<IpInfo>
    {
        Task<StatisticsDTO> GetStatisticsAsync(CancellationToken cancellationToken = default);
    }
}
