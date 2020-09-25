using Luna.Web.Mvc.Middleware;
using Microsoft.AspNetCore.Builder;

namespace Luna.Web
{
    public static class LunaAppBuilderExtensions
    {
        public static IApplicationBuilder UseLuna(this IApplicationBuilder app)
        {
            app.UseMiddleware<LunaExceptionLogMiddleware>();

            return app;
        }
    }
}