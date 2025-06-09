namespace Application.Exceptions
{
    public class StatisticsNotAvailableException : Exception
    {
        public StatisticsNotAvailableException()
            : base("No se pudieron obtener las estadísticas")
        {
        }
    }
}
