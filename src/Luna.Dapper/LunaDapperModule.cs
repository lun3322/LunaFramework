using Luna.Dependency;
using Microsoft.Extensions.DependencyInjection;

namespace Luna.Dapper
{
    [Dependency(typeof(LunaCoreModule))]
    public class LunaDapperModule : LunaModule
    {
        public override void ConfigureServices(IServiceCollection services)
        {
        }
    }
}