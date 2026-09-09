using System.Diagnostics;

public class ErrorHandlerMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ErrorHandlerMiddleware> _logger;

    public ErrorHandlerMiddleware(RequestDelegate next, ILogger<ErrorHandlerMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task Invoke(HttpContext context)
    {
        var stopwatch = Stopwatch.StartNew();
        var username = context.User.Identity?.Name ?? "Anonymous";
        var method = context.Request.Method;
        var path = context.Request.Path.ToString();

        try
        {
            await _next(context);
        }
        catch (Exception error)
        {
            var response = context.Response;
            response.StatusCode = error switch
            {
                UnauthorizedAccessException => StatusCodes.Status401Unauthorized,
                KeyNotFoundException => StatusCodes.Status404NotFound,
                ArgumentException => StatusCodes.Status400BadRequest,
                _ => StatusCodes.Status500InternalServerError
            };
            response.ContentType = "application/json";
            var result = System.Text.Json.JsonSerializer.Serialize(new
            {
                status = false,
                error = error.Message
            });
            _logger.LogError(error, "Request {Method} {Path} by {Username} failed with {StatusCode}",
                method, path, username, response.StatusCode);
            await response.WriteAsync(result);
        }
        finally
        {
            stopwatch.Stop();
            _logger.LogInformation("HTTP {Method} {Path} by {Username} responded {StatusCode} in {ElapsedMilliseconds} ms",
                method, path, username, context.Response.StatusCode, stopwatch.Elapsed.TotalMilliseconds);
        }
    }
}