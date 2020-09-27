using Luna.Dependency;
using Microsoft.Extensions.DependencyInjection;

namespace Luna.Dapper.Tests
{
    [Dependency(typeof(LunaDapperModule))]
    public class LunaDapperTestsModule : LunaModule
    {
        public override void ConfigureServices(IServiceCollection services)
        {
        }
    }
}