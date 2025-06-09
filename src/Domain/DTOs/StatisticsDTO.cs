namespace Domain.DTOs
{
    public class StatisticsDTO
    {
        public int AverageDistanceInvocations { get; set; } = 0;

        public string MaxCountryName { get; set; } = string.Empty;

        public string MaxCountryCode { get; set; } = string.Empty;

        public int MaxDistanceToBuenosAiresKm { get; set; } = 0;

        public int MaxInvocationTimes { get; set; } = 0;

        public string MinCountryName { get; set; } = string.Empty;

        public string MinCountryCode { get; set; } = string.Empty;

        public int MinDistanceToBuenosAiresKm { get; set; } = 0;

        public int MinInvocationTimes { get; set; } = 0;
    }
}
