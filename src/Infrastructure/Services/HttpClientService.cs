namespace Infrastructure.Services
{
    using System.Collections.Generic;
    using System.Net.Http;
    using System.Threading.Tasks;
    using Infrastructure.Exceptions;
    using Microsoft.AspNetCore.WebUtilities;
    using Newtonsoft.Json.Linq;

    public class HttpClientService
    {
        private readonly IHttpClientFactory httpClientFactory;

        public HttpClientService(IHttpClientFactory httpClientFactory)
        {
            this.httpClientFactory = httpClientFactory;
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

            return JObject.Parse(apiResponse);
        }
    }
}
