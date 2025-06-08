namespace Domain.Entities
{
    using Domain.Abstractions;

    public class Country : Entity
    {
        required public string Name { get; set; }

        required public string ISOCode { get; set; }

        // Maybe in the future this could be another entity
        public string? Languages { get; set; }

        required public decimal Latitude { get; set; }

        required public decimal Longitude { get; set; }

        required public string Currency { get; set; }

        required public string TimeZone { get; set; }

        required public int DistanceToBuenosAiresInKm { get; set; }

        public List<IpInfo>? IpInfoRequests { get; set; } = [];
    }
}
