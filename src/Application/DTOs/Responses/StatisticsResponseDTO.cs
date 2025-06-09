namespace Application.DTOs.Responses
{
    public class StatisticsResponseDTO
    {
        required public CountryStatisticsDTO MaxDistanceCountry { get; set; }

        required public CountryStatisticsDTO MinDistanceCountry { get; set; }

        required public int AverageDistanceInvocations { get; set; }
    }
}
