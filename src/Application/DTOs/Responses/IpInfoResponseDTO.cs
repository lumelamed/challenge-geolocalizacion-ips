namespace Application.DTOs.Responses
{
    public class IpInfoResponseDTO
    {
        required public string IP { get; set; }

        public DateTime CurrentDate { get; set; } = DateTime.Now;

        required public string CountryName { get; set; }

        required public string ISOCode { get; set; }

        required public List<string> Languages { get; set; } = [];

        required public string Currency { get; set; }

        required public List<TimeOnly> CurrentTimes { get; set; } = [];

        public decimal? ExchangeRateToUSD { get; set; }

        required public int DistanceToBuenosAiresKm { get; set; }

        required public double Latitude { get; set; }

        required public double Longitude { get; set; }
    }
}
