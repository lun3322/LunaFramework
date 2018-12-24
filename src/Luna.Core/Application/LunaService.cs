using Castle.Core.Logging;
using Luna.Caching;
using Luna.Dependency;

namespace Luna.Application
{
    public abstract class LunaService : ILunaService
    {
        public ILogger Logger { protected get; set; }
        public ILunaCaching Cache { get; set; }
        public IIocManager IocManager { get; set; }

        protected LunaService()
        {
            Logger = NullLogger.Instance;
        }
    }
}