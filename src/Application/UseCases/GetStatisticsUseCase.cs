namespace Application.UseCases
{
    using System.Threading.Tasks;
    using Application.DTOs.Responses;
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
            // TODO
            return new StatisticsResponseDTO
            {
                AverageDistanceInvocations = 0,
                MaxDistanceCountry = new CountryStatisticsDTO { CountryName = string.Empty, DistanceToBuenosAiresKm = 0, InvocationTimes = 0 },
                MinDistanceCountry = new CountryStatisticsDTO { CountryName = string.Empty, DistanceToBuenosAiresKm = 0, InvocationTimes = 0 },
            };
        }
    }
}
