using Microsoft.Extensions.DependencyInjection;

namespace Luna.Dependency
{
    public abstract class LunaModule
    {
        public abstract void ConfigureServices(IServiceCollection services);
    }
}