using Luna.Dapper;
using Luna.Dependency;
using Microsoft.Extensions.DependencyInjection;

namespace TestModule.Core
{
    [Dependency(typeof(LunaModule),
        typeof(LunaDapperModule)
    )]
    public class TestModuleCoreModule : LunaModule
    {
        public override void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<SubModuleRegisteredDependency>();
        }
    }
}