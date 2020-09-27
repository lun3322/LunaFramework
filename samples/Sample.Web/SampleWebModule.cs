using Luna;
using Luna.Dependency;
using Luna.Web;
using Microsoft.Extensions.DependencyInjection;

namespace Sample.Web
{
    [Dependency(typeof(LunaWebModule), typeof(LunaCoreModule))]
    public class SampleWebModule : LunaModule
    {
        public override void ConfigureServices(IServiceCollection services)
        {
        }
    }
}