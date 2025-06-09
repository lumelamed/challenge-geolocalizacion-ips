namespace Application.Interfaces
{
    using Application.DTOs.ExternalAPIs;

    public interface ICurrencyApiClient
    {
        Task<RateResultDTO?> GetDollarExchangeRateAsync(string currencyCode);
    }
}
