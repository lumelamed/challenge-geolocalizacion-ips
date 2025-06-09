namespace Application.UseCases
{
    using System.Threading.Tasks;
    using Application.DTOs.Responses;
    using Application.Exceptions;
    using Domain.Abstractions;

    public class GetStatisticsUseCase
    {
        public GetStatisticsUseCase(IUnitOfWork unitOfWork)
        {
            this.UnitOfWork = unitOfWork;
        }

        private IUnitOfWork UnitOfWork { get; }

        public async Task<StatisticsResponseDTO> ExecuteAsync(CancellationToken cancellationToken)
        {
            var stats = await this.UnitOfWork.IpInfoRepository.GetStatisticsAsync(cancellationToken);

            if (stats is null)
            {
                throw new StatisticsNotAvailableException();
            }

            return new StatisticsResponseDTO
            {
                AverageDistanceInvocations = stats.AverageDistanceInvocations,
                MaxDistanceCountry = new CountryStatisticsDTO
                {
                    CountryName = $"{stats.MaxCountryName} ({stats.MaxCountryCode})",
                    DistanceToBuenosAiresKm = stats.MaxDistanceToBuenosAiresKm,
                    InvocationTimes = stats.MaxInvocationTimes,
                },
                MinDistanceCountry = new CountryStatisticsDTO
                {
                    CountryName = $"{stats.MinCountryName} ({stats.MinCountryCode})",
                    DistanceToBuenosAiresKm = stats.MinDistanceToBuenosAiresKm,
                    InvocationTimes = stats.MinInvocationTimes,
                },
            };
        }
    }
}
