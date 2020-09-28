using Luna.Dependency;
using Microsoft.Extensions.DependencyInjection;

namespace Luna.Redis.AspNetCore.Tests
{
    [Dependency(
        typeof(LunaRedisAspNetCoreModule)
    )]
    public class LunaRedisAspNetCoreTestsModule : LunaModule
    {
        public override void ConfigureServices(IServiceCollection services)
        {
        }
    }
}