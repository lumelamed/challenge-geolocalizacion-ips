namespace Application
{
    using Application.UseCases;
    using Microsoft.Extensions.DependencyInjection;

    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddScoped<GetCountryInfoUseCase>();
            services.AddScoped<GetStatisticsUseCase>();

            return services;
        }
    }
}
