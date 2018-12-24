using System;
using Castle.Core.Logging;
using Luna.Dependency;

namespace Luna
{
    public class LunaStarter : IDisposable
    {
        private ILogger _logger;
        public IIocManager IocManager { get; private set; }

        private LunaStarter(Type entryType, LunaStarterOption option)
        {
            IocManager = option.IocManager;

            IocManager.RegisterAssemblyByConvention(GetType().Assembly);
            IocManager.RegisterAssemblyByConvention(entryType.Assembly);

            _logger = NullLogger.Instance;
        }

        public static LunaStarter Create<T>(LunaStarterOption option = null)
            where T : class
        {
            return new LunaStarter(typeof(T), option ?? new LunaStarterOption());
        }

        public void Initialize()
        {
            ResolveLogger();

            var configurations = IocManager.IocContainer.ResolveAll<ILunaConfiguration>();
            _logger.Info($"找到 {configurations.Length} 个 LunaConfiguration");

            foreach (var configuration in configurations)
            {
                configuration.Initialize();
            }

            foreach (var configuration in configurations)
            {
                configuration.Setup();
            }
        }

        public void Dispose()
        {
            IocManager?.Dispose();
        }

        private void ResolveLogger()
        {
            if (IocManager.IsRegistered<ILoggerFactory>())
            {
                _logger = IocManager.Resolve<ILoggerFactory>().Create(typeof(LunaStarter));
            }
        }
    }
}