namespace Domain.Abstractions
{
    public class ApiError
    {
        public ApiError(string code, string message)
        {
            this.Code = code;
            this.Message = message;
        }

        public string Code { get; set; }

        public string Message { get; set; }
    }
}
