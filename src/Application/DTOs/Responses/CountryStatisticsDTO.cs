namespace Application.DTOs.Responses
{
    public class CountryStatisticsDTO
    {
        public string CountryName { get; set; } = "PAIS NO ENCONTRADO";

        public int DistanceToBuenosAiresKm { get; set; } = 0;

        public int InvocationTimes { get; set; } = 0;
    }
}
