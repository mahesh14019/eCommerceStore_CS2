using Serilog.Core;
using System.Net;
using WebStore.Models.ViewModel;

namespace WebStore.CustomMiddlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly Logger _logger;
        public ExceptionMiddleware(RequestDelegate next, Logger logger)
        {
            _next = next;
            _logger = logger;
        }
        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                await HandleGlobalExceptionAsync(httpContext, ex,_logger);
            }
        }

        private static Task HandleGlobalExceptionAsync(HttpContext context, Exception exception, Logger logger)
        {
            //Technical Exception for troubleshooting
            logger.Error(exception.Message);
            
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            // Business exception for users with gracefull exit message.
            return context.Response.WriteAsync(new ErrorDetails()
            {
                StatusCode = context.Response.StatusCode,
                Message = "Something went wrong !Internal Server Error"
            }.ToString());
        }
    }
}
