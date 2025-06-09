namespace Infrastructure
{
    using Application.Interfaces;
    using Domain.Interfaces;
    using Infrastructure.Repositories;
    using Infrastructure.Services.External;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;

    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(
            this IServiceCollection services,
            string connectionString)
        {
            services.AddHttpClient("FixerClient", c =>
            {
                c.BaseAddress = new Uri("https://data.fixer.io/api/");
            });

            services.AddHttpClient("IpLocationClient", c =>
            {
                c.BaseAddress = new Uri("https://api.ipapi.com/");
            });

            services.AddHttpClient("CountryLayerClient", c =>
            {
                c.BaseAddress = new Uri("https://api.countrylayer.com/v2/");
            });

            services.AddScoped<IIpApiClient, IpLocationClient>();
            services.AddScoped<ICurrencyApiClient, FixerClient>();
            services.AddScoped<ICountryApiClient, CountryLayerClient>();

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
