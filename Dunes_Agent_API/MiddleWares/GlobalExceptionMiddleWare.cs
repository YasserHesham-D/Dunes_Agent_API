using System.Net;
using System.Text.Json;

namespace Presentation.MiddleWares
{
    public class GlobalExceptionMiddleWare
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<GlobalExceptionMiddleWare> _logger;

        public GlobalExceptionMiddleWare(RequestDelegate next, ILogger<GlobalExceptionMiddleWare> logger)
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
                _logger.LogError(ex, "Unhandled exception occurred");

                context.Response.ContentType = "application/json";
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

                var errorResponse = new
                {
                    status = context.Response.StatusCode,
                    message = "An unexpected error occurred. Please try again later.",
                    detail = ex.Message
                };

                var json = JsonSerializer.Serialize(errorResponse);
                await context.Response.WriteAsync(json);
            }
        }

    }
}

