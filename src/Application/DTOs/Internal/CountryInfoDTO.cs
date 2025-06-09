namespace Application.DTOs.Internal
{
    public class CountryInfoDTO
    {
        required public string CountryName { get; set; }

        required public string ISOCode { get; set; }

        public List<string?> Languages { get; set; } = [];

        public string? Currency { get; set; }

        public List<string?> Timezones { get; set; } = [];

        public int? DistanceToBuenosAiresKm { get; set; }

        public double? Latitude { get; set; }

        public double? Longitude { get; set; }
    }
}
