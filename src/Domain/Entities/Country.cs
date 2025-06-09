namespace Domain.Entities
{
    using Domain.Abstractions;

    public class Country : Entity
    {
        required public string Name { get; set; }

        required public string ISOCode { get; set; }

        public string? Languages { get; set; }

        required public string Timezones { get; set; }

        required public double Latitude { get; set; }

        required public double Longitude { get; set; }

        required public string Currency { get; set; }

        required public int DistanceToBuenosAiresInKm { get; set; }

        public List<IpInfo>? IpInfoRequests { get; set; } = [];
    }
}
