namespace Application.Interfaces
{
    using System.Threading.Tasks;
    using Application.DTOs.Responses;

    public interface IIpLocationService
    {
        Task<IpInfoResponseDTO> GetCompleteIpInfo(string ip);
    }
}
