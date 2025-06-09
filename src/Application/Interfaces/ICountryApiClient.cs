namespace Application.Interfaces
{
    using Application.DTOs.ExternalAPIs;

    public interface ICountryApiClient
    {
        Task<CountryResultDTO> GetCountryAsync(string countryCode);
    }
}
