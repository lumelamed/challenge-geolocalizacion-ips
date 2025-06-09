namespace WebApi.Middlewares
{
    using Application.Exceptions;
    using Domain.Abstractions;
    using Infrastructure.Exceptions;

    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate next;

        private readonly ILogger<ExceptionHandlingMiddleware> logger;

        public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
        {
            this.next = next;
            this.logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await this.next(context);
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, $"Ocurrió una excepción: {ex.Message}");

                var (statusCode, error) = MapExceptionToError(ex);

                context.Response.StatusCode = statusCode;

                await context.Response.WriteAsJsonAsync(ApiResponse.Failure(error));
            }
        }

        private static (int statusCode, ApiError error) MapExceptionToError(Exception ex)
        {
            return ex switch
            {
                ArgumentException e => (
                    StatusCodes.Status400BadRequest,
                    new ApiError("INVALID_INPUT", e.Message)),
                CountryNotFoundException e => (
                    StatusCodes.Status404NotFound,
                    new ApiError("COUNTRY_NOT_FOUND", e.Message)),
                StatisticsNotAvailableException e => (
                    StatusCodes.Status404NotFound,
                    new ApiError("STATS_NOT_AVAILABLE", e.Message)),
                ExternalServiceException e => (
                    StatusCodes.Status503ServiceUnavailable,
                    new ApiError("EXTERNAL_SERVICE_ERROR", "Uno de los servicios externos no está disponible.")),
                _ => (
                    StatusCodes.Status500InternalServerError,
                    new ApiError("GENERIC_FAILURE", ex.Message))
            };
        }
    }
}