namespace WebApi.Middlewares
{
    using Microsoft.AspNetCore.Mvc;

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

                var problemDetails = new ProblemDetails
                {
                    Status = StatusCodes.Status500InternalServerError,
                    Type = ex.GetType().FullName,
                    Title = ex.Message,
                    Detail = ex.StackTrace,
                };

                context.Response.StatusCode = problemDetails.Status.Value;

                await context.Response.WriteAsJsonAsync(problemDetails);
            }
        }
    }
}