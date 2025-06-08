namespace Application.Interfaces
{
    using Application.DTOs;

    public interface IIpApiClient
    {
        Task<IpLocationResultDTO> GetLocationAsync(string ip);
    }
}
