using System.Net;
using System.Text.Json;

namespace GameAPI.Middleware
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlingMiddleware> _logger;

        public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error: {Message}", ex.Message);
                await HandleExceptionAsync(context, ex);
            }
        }

        private static async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";

            var response = exception switch
            {
                UnauthorizedAccessException => new ErrorResponse(
                    (int)HttpStatusCode.Unauthorized,
                    "No access."),

                KeyNotFoundException => new ErrorResponse(
                    (int)HttpStatusCode.NotFound,
                    "Resource not found."),

                ArgumentException => new ErrorResponse(
                    (int)HttpStatusCode.BadRequest,
                    exception.Message),

                _ => new ErrorResponse(
                    (int)HttpStatusCode.InternalServerError,
                    "An unexpected error has occurred. Please try again.")
            };

            context.Response.StatusCode = response.StatusCode;

            var json = JsonSerializer.Serialize(response, new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            });

            await context.Response.WriteAsync(json);
        }
    }

    public record ErrorResponse(int StatusCode, string Message);
}