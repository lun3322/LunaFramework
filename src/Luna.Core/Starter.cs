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
        public readonly WindsorContainer Container;
        private ILogger Logger { get; set; }

        private Starter(Type runnerType, StarterOption option)
        {
            Container = new WindsorContainer();

            RegisterAssembly(GetType().Assembly);
            RegisterAssembly(runnerType.Assembly);

            Container.Register(
                Component.For<Starter>().Instance(this).LifestyleSingleton(),
                Component.For<StarterOption>().Instance(option).LifestyleSingleton()
            );

            IocManager.Container = Container;
        }

        private void RegisterAssembly(Assembly assembly)
        {
            Container.Register(
                Classes.FromAssemblyInThisApplication(assembly)
                    .IncludeNonPublicTypes()
                    .BasedOn<ITransientDependency>()
                    .WithServiceAllInterfaces()
                    .If(type => !type.IsGenericTypeDefinition)
                    .WithService.Self()
                    .WithService.DefaultInterfaces()
                    .LifestyleTransient()
            );

            Container.Register(
                Classes.FromAssemblyInThisApplication(assembly)
                    .IncludeNonPublicTypes()
                    .BasedOn<ISingletonDependency>()
                    .If(type => !type.IsGenericTypeDefinition)
                    .WithService.Self()
                    .WithService.DefaultInterfaces()
                    .LifestyleSingleton()
            );
        }

        public static Starter Create<T>(StarterOption option = null)
            where T : IRunner
        {
            return new Starter(typeof(T), option ?? new StarterOption());
        }

        public void Run(bool debug = false)
        {
            try
            {
                Logger = Container.Kernel.HasComponent(typeof(ILogger))
                    ? Container.Resolve<ILogger>()
                    : NullLogger.Instance;

                var runner = Container.Resolve<IRunner>();
                runner.Init();
                runner.Run();
            }
            catch (Exception ex)
            {
                Logger.Warn(ex.ToString());
                if (debug)
                {
                    throw;
                }
            }
        }

        public void Dispose()
        {
            Container.Resolve<IRunner>().Stop();
            Container?.Dispose();
        }
    }
}
