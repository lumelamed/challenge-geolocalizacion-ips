namespace Infrastructure.Services
{
    using System.Threading.Tasks;
    using Application.DTOs.Internal;
    using Application.Interfaces;

    public class LocationService : ILocationService
    {
        public LocationService(
            IIpApiClient ipApiClient,
            ICountryApiClient countryApiClient,
            ICurrencyApiClient currencyApiClient,
            IGeoDistanceService geoDistanceService)
        {
            this.IpApiClient = ipApiClient;
            this.CountryApiClient = countryApiClient;
            this.CurrencyApiClient = currencyApiClient;
            this.GeoDistanceService = geoDistanceService;
        }

        private IIpApiClient IpApiClient { get; }

        private ICountryApiClient CountryApiClient { get; }

        private ICurrencyApiClient CurrencyApiClient { get; }

        private IGeoDistanceService GeoDistanceService { get; }

        public async Task<string?> GetCountryCodeByIP(string ip)
        {
            return (await this.IpApiClient.GetLocationAsync(ip))?.CountryCode;
        }

        public async Task<decimal?> GetDollarExchangeRateByCurrencyCode(string currencyCode)
        {
            return (await this.CurrencyApiClient.GetDollarExchangeRateAsync(currencyCode))?.RateToUsd;
        }

        public async Task<CountryInfoDTO> GetCountryInfoByCode(string countryCode)
        {
            var country = await this.CountryApiClient.GetCountryAsync(countryCode);

            return new CountryInfoDTO
            {
                CountryName = country.Name,
                ISOCode = country.ISOCode,
                Languages = country.Languages,
                Timezones = country.Timezones,
                Currency = country.CurrencyCode,
                Latitude = country.Latitude,
                Longitude = country.Longitude,
                DistanceToBuenosAiresKm = country.Latitude.HasValue && country.Longitude.HasValue
                ? this.GeoDistanceService.CalculateEstimatedDistanceToBuenosAiresKm(country.Latitude.Value, country.Longitude.Value)
                : null,
            };
        }
    }
}
