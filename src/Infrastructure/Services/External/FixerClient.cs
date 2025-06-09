namespace Infrastructure.Services.External
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Application.DTOs.ExternalAPIs;
    using Application.Interfaces;
    using Microsoft.Extensions.Configuration;
    using Newtonsoft.Json.Linq;

    public class FixerClient : ICurrencyApiClient
    {
        private readonly HttpClientService httpClientService;
        private readonly string apiKey;

        public FixerClient(HttpClientService httpClientService, IConfiguration config)
        {
            this.httpClientService = httpClientService;

            var section = config.GetSection("ExternalAPIKeys:Fixer:ApiKey");

            if (section?.Value == null)
            {
                throw new Exception("La API de Currency no tiene un valor de API Key asignado en la configuración");
            }

            this.apiKey = section.Value;
        }

        public async Task<RateResultDTO?> GetDollarExchangeRateAsync(string currencyCode)
        {
            var json = await this.httpClientService.GetRawJsonAsync(
                clientName: "FixerClient",
                path: "latest",
                queryParams: new Dictionary<string, string?>
                {
                    ["access_key"] = this.apiKey,
                    ["base"] = currencyCode,
                    ["symbols"] = "USD",
                });

            var usdRate = json?["rates"]?["USD"]?.Value<decimal?>();

            return usdRate == null ? null : new RateResultDTO
            {
                RateToUsd = usdRate.Value,
            };
        }
    }
}
