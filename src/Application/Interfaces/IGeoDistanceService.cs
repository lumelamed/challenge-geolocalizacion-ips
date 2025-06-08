namespace Application.Interfaces
{
    public interface IGeoDistanceService
    {
        int CalculateEstimatedDistanceToBuenosAiresKm(double fromLat, double fromLon);
    }
}
