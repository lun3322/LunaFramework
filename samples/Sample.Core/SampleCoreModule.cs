using Luna;
using Luna.Dapper;
using Luna.Dependency;
using Microsoft.Extensions.DependencyInjection;

namespace Sample.Core
{
    [Dependency(typeof(LunaCoreModule),
        typeof(LunaDapperModule)
    )]
    public class SampleCoreModule : LunaModule
    {
        public override void ConfigureServices(IServiceCollection services)
        {
        }
    }
}