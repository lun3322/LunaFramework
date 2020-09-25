using Luna.Dependency;
using Microsoft.Extensions.DependencyInjection;

namespace Luna.Web
{
    [Dependency(typeof(LunaCoreModule))]
    public class LunaWebModule : LunaModule
    {
        public override void ConfigureServices(IServiceCollection services)
        {
        }
    }
}