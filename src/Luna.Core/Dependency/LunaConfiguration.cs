using Castle.Core.Logging;

namespace Luna.Dependency
{
    public abstract class LunaConfiguration : ILunaConfiguration
    {
        public ILogger Logger { get; set; }

        public IIocManager IocManager { get; set; }

        protected LunaConfiguration()
        {
            Logger = NullLogger.Instance;
        }

        public abstract void Initialize();

        public abstract void Setup();
    }
}