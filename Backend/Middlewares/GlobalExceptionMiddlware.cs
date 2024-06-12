using Microsoft.AspNetCore.Mvc;

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

        #region Methods: Public

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, "Exception occurred: {Message}", exception.Message);

                var problemDetails = new ProblemDetails
                {
                    Status = StatusCodes.Status500InternalServerError,
                    Title = "Server Error"
                };

                context.Response.StatusCode = StatusCodes.Status500InternalServerError;

                await context.Response.WriteAsJsonAsync(problemDetails);
            }
        }

        #endregion
    }
}
