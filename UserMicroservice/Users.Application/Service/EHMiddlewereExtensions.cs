using Microsoft.AspNetCore.Builder;

namespace Users.Service
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
