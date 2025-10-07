using System.Text.Json;
using Microsoft.AspNetCore.Http;
using System.Reflection;
using ECommerce.Helper;

namespace ECommerce.Middelware
{
    public class ResponseHandlerMiddleware
    {
        private readonly RequestDelegate _next;

        public ResponseHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            // Temporarily capture the response
            var originalBodyStream = context.Response.Body;
            using var memoryStream = new MemoryStream();
            context.Response.Body = memoryStream;

            await _next(context); // Call the next middleware (eventually reaches controller)

            memoryStream.Seek(0, SeekOrigin.Begin);
            var responseBody = await new StreamReader(memoryStream).ReadToEndAsync();
            memoryStream.Seek(0, SeekOrigin.Begin);

            // Try to detect if it's a ResponseHandler<TStatus, TData> object
            if (!string.IsNullOrWhiteSpace(responseBody))
            {
                try
                {
                    var responseJson = JsonDocument.Parse(responseBody);
                    if (responseJson.RootElement.TryGetProperty("code", out var codeElement) && codeElement.ValueKind == JsonValueKind.Number)
                    {
                        var statusCode = codeElement.GetInt32();
                        context.Response.StatusCode = statusCode; // Override the status code
                    }
                }
                catch
                {
                    // If not JSON or not ResponseHandler, ignore and continue
                }
            }

            // Write back the response
            context.Response.Body = originalBodyStream;
            await context.Response.WriteAsync(responseBody);
        }
    }

}
