namespace Application.Interfaces
{
    using System.Threading.Tasks;
    using Application.DTOs.Internal;

    public interface ILocationService
    {
        Task<string?> GetCountryCodeByIP(string ip);

        Task<decimal?> GetDollarExchangeRateByCurrencyCode(string currencyCode);

        Task<CountryInfoDTO> GetCountryInfoByCode(string countryCode);
    }
}
