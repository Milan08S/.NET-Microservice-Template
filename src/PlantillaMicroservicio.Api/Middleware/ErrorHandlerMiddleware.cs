using System.Net;
using System.Text.Json;

namespace PlantillaMicroservicio.Api.Middleware;

public class ErrorHandlerMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ErrorHandlerMiddleware> _logger;

    public ErrorHandlerMiddleware(RequestDelegate next, ILogger<ErrorHandlerMiddleware> logger)
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
        catch (Exception error)
        {
            var response = context.Response;
            response.ContentType = "application/json";
            response.StatusCode = (int)HttpStatusCode.InternalServerError;

            _logger.LogError(error, "Ocurrió un error no controlado: {ErrorMessage}", error.Message);

            var result = JsonSerializer.Serialize(new { message = "Ocurrió un error interno en el servidor." });
            await response.WriteAsync(result);
        }
    }
}