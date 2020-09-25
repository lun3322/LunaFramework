using Luna.Dependency;
using Microsoft.Extensions.DependencyInjection;

namespace TestModule.Core
{
    [Dependency(typeof(LunaModule))]
    public class TestModuleCoreModule : LunaModule
    {
        public override void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<SubModuleRegisteredDependency>();
        }
    }
}