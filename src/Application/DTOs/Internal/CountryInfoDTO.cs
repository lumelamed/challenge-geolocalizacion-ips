namespace Application.DTOs.Internal
{
    public class CountryInfoDTO
    {
        required public string CountryName { get; set; }

        required public string ISOCode { get; set; }

        required public List<string> Languages { get; set; } = [];

        required public string Currency { get; set; }

        required public List<string> Timezones { get; set; } = [];

        required public int DistanceToBuenosAiresKm { get; set; }

        required public double Latitude { get; set; }

        required public double Longitude { get; set; }
    }
}
