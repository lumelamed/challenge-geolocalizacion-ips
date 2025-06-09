namespace Application.Exceptions
{
    public class CountryNotFoundException : Exception
    {
        public CountryNotFoundException(string ip)
            : base($"No se pudo determinar el país para la IP {ip}")
        {
        }
    }
}
