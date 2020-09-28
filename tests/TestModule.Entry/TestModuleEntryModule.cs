using Luna;
using Luna.Dapper;
using Luna.Dependency;
using Luna.MongoDb;
using Luna.Web;
using Microsoft.Extensions.DependencyInjection;
using TestModule.Core;

namespace TestModule.Entry
{
    [Dependency(typeof(LunaCoreModule),
        typeof(TestModuleCoreModule),
        typeof(LunaMongoDbModule),
        typeof(LunaWebModule)
    )]
    public class TestModuleEntryModule : LunaModule
    {
        public override void ConfigureServices(IServiceCollection services)
        {
        }
    }
}