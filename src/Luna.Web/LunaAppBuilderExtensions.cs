using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Luna.Web
{
    public static class LunaAppBuilderExtensions
    {
        public static IApplicationBuilder UseLuna(this IApplicationBuilder app)
        {
            var starter = app.ApplicationServices.GetRequiredService<LunaStarter>();
            starter.Initialize();

            return app;
        }
    }
}
