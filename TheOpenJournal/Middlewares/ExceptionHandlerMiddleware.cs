using Microsoft.IdentityModel.Tokens;

namespace TheOpenJournal.Middlewares
{
    public class ExceptionHandlerMiddleware
    {
        private readonly ILogger<ExceptionHandlerMiddleware> _logger;
        private readonly RequestDelegate _next;

        public ExceptionHandlerMiddleware(ILogger<ExceptionHandlerMiddleware> logger, RequestDelegate next)
        {
            _logger = logger;
            _next = next;
        }
        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                Guid errorId = Guid.NewGuid();

                _logger.LogError(ex,$"{errorId} - {ex.Message}");
                httpContext.Response.StatusCode = (int)httpContext.Response.StatusCode;
                httpContext.Response.ContentType = "application/json";
                var error = new
                {
                    Id  = errorId,
                    Message = "Internal Server Error. We'll fix it soon. :)",

                };
                await httpContext.Response.WriteAsJsonAsync(error);
            }
        }
    }
}
