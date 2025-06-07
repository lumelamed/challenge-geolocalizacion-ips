namespace Domain.Abstractions
{
    public class ApiResponse<T>
    {
        protected ApiResponse(bool success, T? data, ApiError? error)
        {
            this.IsSuccess = success;
            this.Error = error;
            this.Data = data;
        }

        public bool IsSuccess { get; private set; }

        public ApiError? Error { get; private set; }

        public T? Data { get; private set; }

        public static ApiResponse<T> Success(T data) => new (true, data, null);

        public static ApiResponse<T> Failure(ApiError message) => new (false, default, message);

        public static ApiResponse<T> Failure(string message) => new (false, default, new ApiError("Error_Generico", message));
    }

    public class ApiResponse
    {
        private ApiResponse(bool success, ApiError? error)
        {
            this.IsSuccess = success;
            this.Error = error;
        }

        public bool IsSuccess { get; private set; }

        public ApiError? Error { get; private set; }

        public static ApiResponse Success() => new (true, null);

        public static ApiResponse Failure(ApiError error) => new (false, error);

        public static ApiResponse Failure(string message) => new (false, new ApiError("Error_Generico", message));
    }
}
