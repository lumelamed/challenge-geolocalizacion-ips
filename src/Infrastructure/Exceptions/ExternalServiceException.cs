namespace Infrastructure.Exceptions
{
    public class ExternalServiceException : Exception
    {
        public ExternalServiceException(string message, Exception? inner = null)
            : base(message, inner)
        {
        }
    }
}
