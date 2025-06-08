namespace Infrastructure
{
    using Domain.Interfaces;
    using Infrastructure.Repositories;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;

    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(
            this IServiceCollection services,
            string connectionString)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(connectionString);
            });

            services.AddScoped<IIpInfoRepository, IpInfoRepository>();
            services.AddScoped<ICountryRepository, CountryRepository>();

            return services;
        }
    }
}
