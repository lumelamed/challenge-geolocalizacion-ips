namespace Infrastructure.Configurations
{
    using Domain.Entities;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class IpInfoConfiguration : IEntityTypeConfiguration<IpInfo>
    {
        public void Configure(EntityTypeBuilder<IpInfo> builder)
        {
            builder.Property(x => x.IP).HasMaxLength(50);

            builder.Property(x => x.RequestDate).HasPrecision(0);

            builder.HasOne(x => x.Country)
                .WithMany(y => y.IpInfoRequests)
                .HasForeignKey(x => x.CountryId);
        }
    }
}
