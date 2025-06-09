namespace Infrastructure.Repositories
{
    using System.Linq;
    using Application.Exceptions;
    using Domain.DTOs;
    using Domain.Entities;
    using Domain.Interfaces;
    using Microsoft.EntityFrameworkCore;

    public sealed class IpInfoRepository : BaseRepository<IpInfo>, IIpInfoRepository
    {
        public IpInfoRepository(ApplicationDbContext dbContext)
            : base(dbContext)
        {
        }

        public async Task<StatisticsDTO> GetStatisticsAsync(CancellationToken cancellationToken = default)
        {
            var grouped = await this.DbContext.IpInfo
             .Include(x => x.Country)
             .GroupBy(q => new { q.CountryId, q.Country!.Name, q.Country!.ISOCode, q.Country!.DistanceToBuenosAiresInKm })
             .Select(g => new
             {
                 g.Key.Name,
                 g.Key.ISOCode,
                 DistanceKm = g.Key.DistanceToBuenosAiresInKm ?? 0,
                 InvocationTimes = g.Count(),
             })
             .ToListAsync(cancellationToken);

            if (grouped is null || grouped.Count == 0)
            {
                throw new StatisticsNotAvailableException();
            }

            var totalInvocations = grouped.Sum(g => g.InvocationTimes);
            var totalWeightedDistance = grouped.Sum(g => g.DistanceKm * g.InvocationTimes);
            var average = totalWeightedDistance / totalInvocations;

            var max = grouped.OrderByDescending(g => g.DistanceKm).First();
            var min = grouped.OrderBy(g => g.DistanceKm).First();

            return new StatisticsDTO
            {
                AverageDistanceInvocations = average,
                MaxCountryCode = max.ISOCode,
                MaxCountryName = max.Name,
                MaxDistanceToBuenosAiresKm = max.DistanceKm,
                MaxInvocationTimes = max.InvocationTimes,
                MinCountryCode = min.ISOCode,
                MinCountryName = min.Name,
                MinDistanceToBuenosAiresKm = min.DistanceKm,
                MinInvocationTimes = min.InvocationTimes,
            };
        }
    }
}
