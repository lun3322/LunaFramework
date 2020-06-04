using Castle.Core.Logging;
using Luna.Dependency;
using System;

namespace Luna
{
    public class LunaStarter : IDisposable
    {
        private ILogger _logger;
        public IIocManager IocManager { get; private set; }

        private LunaStarter(Type entryType, LunaStarterOption option)
        {
            IocManager = option.IocManager;

            IocManager.RegisterAssemblyByConvention(typeof(LunaStarter).Assembly);
            IocManager.RegisterAssemblyByConvention(entryType.Assembly);

            _logger = NullLogger.Instance;
        }

        public static LunaStarter Create<T>(Action<LunaStarterOption> action = null, bool isRun = false)
            where T : class
        {
            var option = new LunaStarterOption();
            if (isRun)
            {
                action?.Invoke(option);
            }

            return new LunaStarter(typeof(T), option);
        }

        public void Initialize()
        {
            ResolveLogger();
            LunaConfigurationRegister.Initialize(IocManager);

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