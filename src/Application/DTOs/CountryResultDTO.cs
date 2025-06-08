namespace Application.DTOs
{
    public class CountryResultDTO
    {
        required public string Name { get; set; }

        required public string ISOCode { get; set; }

        required public List<string> Languages { get; set; } = [];

        required public string CurrencyCode { get; set; }

        required public List<string> Timezones { get; set; } = [];

        required public double Latitude { get; set; }

        required public double Longitude { get; set; }
    }
}
