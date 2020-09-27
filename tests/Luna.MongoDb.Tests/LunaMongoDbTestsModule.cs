using Luna.Dependency;
using Microsoft.Extensions.DependencyInjection;

namespace Luna.MongoDb.Tests
{
    [Dependency(typeof(LunaMongoDbModule))]
    public class LunaMongoDbTestsModule : LunaModule
    {
        public override void ConfigureServices(IServiceCollection services)
        {
        }
    }
}