using Microsoft.AspNetCore.Mvc;
using RestaurantReservationSystem.Domain.Exceptions;
using RestaurantReservationSystem.Domain.Responses;
using System.Text.Json;

namespace RestaurantReservationSystem.API.Middlewares
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

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (NotFoundException ex)
            {
                _logger.LogWarning(ex, "NotFoundException occurred");

                var problemDetails = new ProblemDetails
                {
                    Title = "Resource not found",
                    Status = StatusCodes.Status404NotFound,
                    Detail = ex.Message,
                    Instance = context.Request.Path
                };

                context.Response.StatusCode = StatusCodes.Status404NotFound;
                context.Response.ContentType = "application/problem+json";
                await context.Response.WriteAsJsonAsync(problemDetails);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unhandled exception");

                var problemDetails = new ProblemDetails
                {
                    Title = "An unexpected error occurred",
                    Status = StatusCodes.Status500InternalServerError,
                    Detail = "Internal server error",
                    Instance = context.Request.Path
                };

                context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                context.Response.ContentType = "application/problem+json";
                await context.Response.WriteAsJsonAsync(problemDetails);
            }
        }
    }
}