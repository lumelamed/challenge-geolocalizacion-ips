namespace Infrastructure.Services
{
    using System;
    using System.Threading.Tasks;
    using Application.DTOs.Responses;
    using Application.Interfaces;

    public class IpLocationService : IIpLocationService
    {
        private readonly IIpApiClient ipApiClient;
        private readonly ICountryApiClient countryApiClient;
        private readonly ICurrencyApiClient currencyApiClient;
        private readonly IGeoDistanceService geoDistanceService;

        public IpLocationService(
            IIpApiClient ipApiClient,
            ICountryApiClient countryApiClient,
            ICurrencyApiClient currencyApiClient,
            IGeoDistanceService geoDistanceService)
        {
            this.ipApiClient = ipApiClient;
            this.countryApiClient = countryApiClient;
            this.currencyApiClient = currencyApiClient;
            this.geoDistanceService = geoDistanceService;
        }

        public async Task<IpInfoResponseDTO> GetCompleteIpInfo(string ip)
        {
            var ipLocation = await this.ipApiClient.GetLocationAsync(ip);
            var country = await this.countryApiClient.GetCountryAsync(ipLocation.CountryCode);
            var rate = await this.currencyApiClient.GetDollarExchangeRateAsync(country.CurrencyCode);

            var distance = this.geoDistanceService.CalculateEstimatedDistanceToBuenosAiresKm(country.Latitude, country.Longitude);

            return new IpInfoResponseDTO
            {
                IP = ip,
                CountryInfo = new CountryInfoResponseDTO
                {
                    CountryName = country.Name,
                    ISOCode = country.ISOCode,
                    Languages = country.Languages,
                    CurrentTimes = [.. country.Timezones.Select(x => TimeOnly.FromDateTime(TimeZoneInfo.ConvertTime(DateTime.UtcNow, TimeZoneInfo.FindSystemTimeZoneById(x))))],
                    Currency = country.CurrencyCode,
                    ExchangeRateToUSD = rate?.RateToUsd,
                    DistanceToBuenosAiresKm = distance,
                },
            };
        }
    }
}
