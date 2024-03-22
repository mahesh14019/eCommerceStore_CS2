using Serilog.Core;

namespace WebStore.CustomMiddlewares
{
    public static class GlobalExceptionMiddleware
    {
        public static void UseGlobalExceptionMiddleware(this IApplicationBuilder app, Logger logger)
        {
            app.UseMiddleware<ExceptionMiddleware>(logger);
        }
    }
}
