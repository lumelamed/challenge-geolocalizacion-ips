namespace Infrastructure.Configurations
{
    using Domain.Entities;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class CountryConfiguration : IEntityTypeConfiguration<Country>
    {
        public void Configure(EntityTypeBuilder<Country> builder)
        {
            builder.Property(x => x.Name).HasMaxLength(100);
            builder.Property(x => x.ISOCode).HasMaxLength(3);
            builder.Property(x => x.Languages).HasMaxLength(250);
            builder.Property(x => x.Currency).HasMaxLength(10);
            builder.Property(x => x.Timezones).HasMaxLength(250);

            builder.Property(x => x.Latitude).HasPrecision(5, 2);
            builder.Property(x => x.Longitude).HasPrecision(5, 2);
            builder.Property(x => x.DistanceToBuenosAiresInKm).HasPrecision(5, 2);
        }
    }
}
