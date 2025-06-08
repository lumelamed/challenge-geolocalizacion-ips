namespace Application.Interfaces
{
    using Application.DTOs;

    public interface ICurrencyApiClient
    {
        Task<RateResultDTO?> GetDollarExchangeRateAsync(string currencyCode);
    }
}
