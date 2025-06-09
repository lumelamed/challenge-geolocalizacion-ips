namespace Application.DTOs.ExternalAPIs
{
    public class CountryResultDTO
    {
        required public string Name { get; set; }

        required public string ISOCode { get; set; }

        public List<string?> Languages { get; set; } = [];

        public string? CurrencyCode { get; set; }

        public List<string?> Timezones { get; set; } = [];

        public double? Latitude { get; set; }

        public double? Longitude { get; set; }
    }
}
