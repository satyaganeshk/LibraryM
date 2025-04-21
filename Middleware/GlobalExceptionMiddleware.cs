using Microsoft.AspNetCore.Http;
using System.Net;
using System.Text.Json;

namespace LibraryM.Middleware
{
    public class GlobalExceptionMiddleware
    {
        // RequestDelegate is a built-in type in ASP.NET Core
        // It represents the next middleware in the HTTP pipeline
        private readonly RequestDelegate _next;

        // Constructor to receive and store the next middleware delegate
        public GlobalExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        // This method is automatically called during request processing
        // 'HttpContext' represents all HTTP-specific information about the request
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                // Let the request proceed to the next middleware/controller
                await _next(context);
            }
            catch (Exception ex)
            {
                // Handle any exception that bubbles up from downstream
                await HandleExceptionAsync(context, ex);
            }
        }

        // This method builds and sends a consistent error response
        private Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            // Set the response content type and status code to 500 (Internal Server Error)
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            // Prepare a standard error response in JSON format
            var response = new
            {
                StatusCode = context.Response.StatusCode,
                Message = "An unexpected error occurred.",
                Detail = exception.Message // For debugging; hide in production
            };

            // Serialize the response object into JSON string
            var json = JsonSerializer.Serialize(response);

            // Write the JSON string into the HTTP response body
            return context.Response.WriteAsync(json);
        }
    }
}
