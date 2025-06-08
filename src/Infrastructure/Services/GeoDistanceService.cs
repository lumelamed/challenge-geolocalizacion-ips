namespace Infrastructure.Services
{
    using System;
    using Application.Interfaces;

    public class GeoDistanceService : IGeoDistanceService
    {
        private const double EarthRadiusKm = 6371.0;
        private const double BuenosAiresLat = -34.6037;
        private const double BuenosAiresLon = -58.3816;

        public int CalculateEstimatedDistanceToBuenosAiresKm(double fromLat, double fromLon)
        {
            return (int)CalculateDistanceKm(fromLat, fromLon, BuenosAiresLat, BuenosAiresLon);
        }

        private static double CalculateDistanceKm(double fromLat, double fromLon, double toLat, double toLon)
        {
            // Convertir grados a radianes
            double lat1Rad = DegreesToRadians(fromLat);
            double lon1Rad = DegreesToRadians(fromLon);
            double lat2Rad = DegreesToRadians(toLat);
            double lon2Rad = DegreesToRadians(toLon);

            // Diferencias
            double deltaLat = lat2Rad - lat1Rad;
            double deltaLon = lon2Rad - lon1Rad;

            // Fórmula de Haversine
            double a = Math.Pow(Math.Sin(deltaLat / 2), 2) +
                       (Math.Cos(lat1Rad) * Math.Cos(lat2Rad) *
                       Math.Pow(Math.Sin(deltaLon / 2), 2));

            double c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
            double distance = EarthRadiusKm * c;

            return Math.Round(distance, 2);
        }

        private static double DegreesToRadians(double degrees)
        {
            return degrees * Math.PI / 180.0;
        }
    }
}
