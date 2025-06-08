namespace Application.DTOs.Responses
{
    public class IpInfoResponseDTO
    {
        required public string IP { get; set; }

        public DateTime CurrentDate { get; set; } = DateTime.Now;

        required public CountryInfoResponseDTO CountryInfo { get; set; }
    }
}
