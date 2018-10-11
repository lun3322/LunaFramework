using System;
using System.Reflection;
using Castle.Core.Logging;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using Luna.Dependency;

namespace Luna
{
    public class LunaStarter : IDisposable
    {
        private ILogger Logger { get; set; }
        public IIocManager IocManager { get; private set; }

        private LunaStarter(Type entryType, LunaStarterOption option)
        {
            IocManager = option.IocManager;

            IocManager.RegisterAssemblyByConvention(GetType().Assembly);
            IocManager.RegisterAssemblyByConvention(entryType.Assembly);
        }

        public static LunaStarter Create<T>(LunaStarterOption option = null)
            where T : class
        {
            return new LunaStarter(typeof(T), option ?? new LunaStarterOption());
        }

        public void Initialize()
        {

        }

        public void Dispose()
        {
            IocManager?.Dispose();
        }
    }
}
