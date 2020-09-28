using Luna.Dependency;
using Microsoft.Extensions.DependencyInjection;
using TestModule.Entry;

namespace Luna.Core.Tests
{
    [Dependency(
        typeof(TestModuleEntryModule)
    )]
    public class LunaCoreTestsModule : LunaModule
    {
        public override void ConfigureServices(IServiceCollection services)
        {
        }
    }
}