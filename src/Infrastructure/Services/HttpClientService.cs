namespace Infrastructure.Services
{
    using Infrastructure.Exceptions;
    using Microsoft.AspNetCore.WebUtilities;
    using Microsoft.Extensions.Logging;
    using Newtonsoft.Json.Linq;
    using System.Collections.Generic;
    using System.Net.Http;
    using System.Threading.Tasks;

    public class HttpClientService
    {
        private readonly IHttpClientFactory httpClientFactory;
        private readonly ILogger<HttpClientService> logger;

        public HttpClientService(IHttpClientFactory httpClientFactory, ILogger<HttpClientService> logger)
        {
            this.httpClientFactory = httpClientFactory;
            this.logger = logger;
        }

        public async Task<JObject?> GetRawJsonAsync(
            string clientName,
            string? path = null,
            Dictionary<string, string?>? queryParams = null)
        {
            var client = this.httpClientFactory.CreateClient(clientName);

            string url = path ?? string.Empty;

            if (queryParams is not null)
            {
                url = QueryHelpers.AddQueryString(url, queryParams);
            }

            this.logger.LogWarning($"HttpClientService request: {url}");

            using var response = await client.GetAsync(url);
            string apiResponse = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                throw new ExternalServiceException($"Error de API ({clientName}): {apiResponse}");
            }

            if (string.IsNullOrWhiteSpace(apiResponse))
            {
                throw new ExternalServiceException($"Respuesta vacía de {clientName}");
            }

            this.logger.LogWarning($"HttpClientService response: {apiResponse}");

            return JObject.Parse(apiResponse);
        }
    }
}
