using Luna.Dependency;
using Microsoft.Extensions.DependencyInjection;

namespace Luna.Nlog.AspNetCore
{
    [Dependency(
        typeof(LunaCoreModule)
    )]
    public class LunaNlogAspNetCoreModule : LunaModule
    {
        public override void ConfigureServices(IServiceCollection services)
        {
        }
    }
}