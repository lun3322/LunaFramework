using System;
using System.Reflection;
using Castle.Core.Logging;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using Luna.Dependency;

namespace Luna
{
    public class Starter : IDisposable
    {
        private ILogger Logger { get; set; }
        public IIocManager IocManager { get; private set; }

        private Starter(Type entryType, StarterOption option)
        {
            IocManager = option.IocManager;

            IocManager.RegisterAssemblyByConvention(GetType().Assembly);
            IocManager.RegisterAssemblyByConvention(entryType.Assembly);
        }

        public static Starter Create<T>(StarterOption option = null)
            where T : class
        {
            return new Starter(typeof(T), option ?? new StarterOption());
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
