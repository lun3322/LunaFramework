using Luna.Dependency;
using Microsoft.Extensions.DependencyInjection;

namespace Luna.MongoDb
{
    [Dependency(typeof(LunaCoreModule))]
    public class LunaMongoDbModule : LunaModule
    {
        public override void ConfigureServices(IServiceCollection services)
        {
        }
    }
}