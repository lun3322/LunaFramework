using Luna.Dependency;
using Microsoft.Extensions.DependencyInjection;
using TestModule.Core;

namespace TestModule.Entry
{
    [Dependency(typeof(LunaModule), typeof(TestModuleCoreModule))]
    public class TestModuleEntryModule : LunaModule
    {
        public override void ConfigureServices(IServiceCollection services)
        {
        }
    }
}