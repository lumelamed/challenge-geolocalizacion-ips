namespace Domain.Entities
{
    using Domain.Abstractions;

    public class IpInfo : Entity
    {
        required public string IP { get; set; }

        required public int CountryId { get; set; }

        public Country? Country { get; set; }

        required public DateTime RequestDate { get; set; } = DateTime.UtcNow;
    }
}
