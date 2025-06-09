namespace Infrastructure.Services.External
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Application.DTOs.ExternalAPIs;
    using Application.Interfaces;
    using Microsoft.Extensions.Configuration;
    using Newtonsoft.Json.Linq;

    public class IpLocationClient : IIpApiClient
    {
        private readonly HttpClientService httpClientService;
        private readonly string apiKey;

        public IpLocationClient(HttpClientService httpClientService, IConfiguration config)
        {
            this.httpClientService = httpClientService;

            var section = config.GetSection("ExternalAPIKeys:IpApi:ApiKey");

            if (section?.Value == null)
            {
                throw new Exception("La API de IPs no tiene un valor de API Key asignado en la configuración");
            }

            this.apiKey = section.Value;
        }

        public async Task<IpLocationResultDTO?> GetLocationAsync(string ip)
        {
            var json = await this.httpClientService.GetRawJsonAsync(
                clientName: "IpLocationClient",
                path: ip,
                queryParams: new Dictionary<string, string?>
                {
                    ["access_key"] = this.apiKey,
                });

            var code = json?["country_code"]?.Value<string>();

            return string.IsNullOrWhiteSpace(code) ? null : new IpLocationResultDTO
            {
                CountryCode = code,
            };
        }
    }
}
