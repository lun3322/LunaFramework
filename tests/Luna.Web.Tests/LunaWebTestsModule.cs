using Luna.Dependency;
using Microsoft.Extensions.DependencyInjection;

namespace Luna.Web.Tests
{
    [Dependency(typeof(LunaCoreModule), typeof(LunaWebModule))]
    public class LunaWebTestsModule : LunaModule
    {
        public override void ConfigureServices(IServiceCollection services)
        {
        }
    }
}