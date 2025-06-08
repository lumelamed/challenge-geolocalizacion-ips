namespace Application.Interfaces
{
    using Application.DTOs;

    public interface ICountryApiClient
    {
        Task<CountryResultDTO> GetCountryAsync(string countryCode);
    }
}
