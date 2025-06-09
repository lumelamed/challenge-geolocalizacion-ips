namespace Application.DTOs.Responses
{
    public class CountryStatisticsDTO
    {
        required public string CountryName { get; set; }

        required public int DistanceToBuenosAiresKm { get; set; }

        required public int InvocationTimes { get; set; }
    }
}
