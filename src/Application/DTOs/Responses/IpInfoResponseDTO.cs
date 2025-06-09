namespace Application.DTOs.Responses
{
    public class IpInfoResponseDTO
    {
        required public string IP { get; set; }

        public DateTime CurrentDate { get; set; } = DateTime.Now;

        required public string CountryName { get; set; }

        required public string ISOCode { get; set; }

        public List<string> Languages { get; set; } = [];

        public string? Currency { get; set; }

        public List<TimeOnly> CurrentTimes { get; set; } = [];

        public decimal? ExchangeRateToUSD { get; set; }

        public int? DistanceToBuenosAiresKm { get; set; }

        public double? Latitude { get; set; }

        public double? Longitude { get; set; }
    }
}
