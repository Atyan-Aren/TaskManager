using Microsoft.AspNetCore.Mvc;
using TaskManager.Exceptions;

namespace TaskManager.Middlewares
{
    public class GlobalExceptionMiddlware
    {
        #region Fields

        private readonly RequestDelegate _next;
        private readonly ILogger<GlobalExceptionMiddlware> _logger;

        #endregion

        #region Constructors

        public GlobalExceptionMiddlware(RequestDelegate next, ILogger<GlobalExceptionMiddlware> logger)
        {
            _next = next;
            _logger = logger;
        }

        #endregion

        #region Methods: Private
        
        private async Task HandleCustomError(HttpContext context, CustomExceptionBase exception)
        {
            _logger.LogError(exception.ToString());

            var problemDetails = new ProblemDetails
            {
                Status = exception.StatusCode,
                Detail = exception.Message
            };

            context.Response.StatusCode = exception.StatusCode;

            await context.Response.WriteAsJsonAsync(problemDetails);
        }

        private async Task HandleBaseError(HttpContext context, Exception exception)
        {
            _logger.LogError($"непредвиденная ошибка:\n{exception.Message}\n{exception.StackTrace}");

            int statusCode = StatusCodes.Status500InternalServerError;
            var problemDetails = new ProblemDetails
            {
                Status = statusCode,
                Detail = exception.Message
            };

            context.Response.StatusCode = statusCode;

            await context.Response.WriteAsJsonAsync(problemDetails);
        }

        #endregion

        #region Methods: Public

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (LoginException loginException)
            {
                await HandleCustomError(context, loginException);
            }
            // Append new custom exceptions
            // Append new custom exceptions
            // Append new custom exceptions
            catch (Exception exception)
            {
                await HandleBaseError(context, exception);
            }
        }

        #endregion
    }
}
