using Microsoft.AspNetCore.Builder;

namespace ServiceCenter.Service
{
    public static class EHMiddlewereExtensions
    {
        public static IApplicationBuilder UseCustomExceptionHandler(this IApplicationBuilder app)
        {
            app.UseMiddleware<ExeptionHendlerMiddleware>();
            return app;
        }
    }
}
