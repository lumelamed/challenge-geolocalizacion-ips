namespace Domain.Entities
{
    using Domain.Abstractions;

    public class Country : Entity
    {
        required public string Name { get; set; }

        required public string ISOCode { get; set; }

        public string? Languages { get; set; }

        public string? Timezones { get; set; }

        public double? Latitude { get; set; }

        public double? Longitude { get; set; }

        public string? Currency { get; set; }

        public int? DistanceToBuenosAiresInKm { get; set; }

        public List<IpInfo>? IpInfoRequests { get; set; } = [];
    }
}
