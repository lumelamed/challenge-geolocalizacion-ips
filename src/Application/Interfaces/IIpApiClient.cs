namespace Application.Interfaces
{
    using Application.DTOs.ExternalAPIs;

    public interface IIpApiClient
    {
        Task<IpLocationResultDTO> GetLocationAsync(string ip);
    }
}
