namespace Infrastructure.Services.External
{
    using Application.DTOs.ExternalAPIs;
    using Application.Interfaces;
    using Microsoft.Extensions.Configuration;
    using Newtonsoft.Json.Linq;

    public class CountryLayerClient : ICountryApiClient
    {
        private readonly HttpClientService httpClientService;
        private readonly string apiKey;

        public CountryLayerClient(HttpClientService httpClientService, IConfiguration config)
        {
            this.httpClientService = httpClientService;

            var section = config.GetSection("ExternalAPIKeys:CountryLayer");

            if (section?.Value == null)
            {
                throw new Exception("La API de Countries no tiene un valor de API Key asignado en la configuración");
            }

            this.apiKey = section.Value;
        }

        public async Task<CountryResultDTO> GetCountryAsync(string isoCode)
        {
            var json = await this.httpClientService.GetRawJsonAsync(
                clientName: "CountryLayerClient",
                path: $"alpha/{isoCode}",
                queryParams: new Dictionary<string, string?>
                {
                    ["access_key"] = this.apiKey,
                    ["filters"] = "name;languages;currencies;timezones;latlng;translations",
                });

            var name = json?["translations"]?["es"]?.Value<string>() ?? json?["name"]?.Value<string>();
            var languages = json?["languages"]?["name"]?.Values<string>()?.ToList();
            var currencyCode = json?["currencies"]?.First?.Path?.Split('.')?.Last();
            var timezones = json?["timezones"]?.Values<string>()?.ToList();
            var latlng = json?["latlng"]?.Values<double>()?.ToArray();

            return new CountryResultDTO
            {
                ISOCode = isoCode,
                Name = name ?? "PAIS NO DISPONIBLE",
                CurrencyCode = currencyCode,
                Languages = languages ?? [],
                Timezones = timezones ?? [],
                Latitude = latlng?[0],
                Longitude = latlng?[1],
            };
        }
    }
}
