using Microsoft.AspNetCore.Http;
using System.Net;
using FluentValidation;
using System.Text.Json;

namespace CustomerOnboarding.Services.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (ValidationException ex)
            {
                await HandleValidationExceptionAsync(context, ex);
            }
            catch (Exception ex)
            {
                await HandleGlobalExceptionAsync(context, ex);
            }
        }

        private async Task HandleValidationExceptionAsync(HttpContext context, ValidationException ex)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.BadRequest;

            // Extracting validation errors from ex.Errors
            var errorResponse = new
            {
                status = "error",
                message = "Validation failed",
                errors = ex.Errors // Use ex.Errors instead of ex.ValidationErrors
            };

            // Manually serialize JSON and write it to response body
            var jsonResponse = JsonSerializer.Serialize(errorResponse);
            await context.Response.WriteAsync(jsonResponse); // Using WriteAsync to send JSON response
        }

        private async Task HandleGlobalExceptionAsync(HttpContext context, Exception ex)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            var errorResponse = new
            {
                status = "error",
                message = "An unexpected error occurred."
            };

            // Manually serialize JSON and write it to response body
            var jsonResponse = JsonSerializer.Serialize(errorResponse);
            await context.Response.WriteAsync(jsonResponse); 
        }
    }
}

