using System.Reflection;
using Castle.MicroKernel.Registration;
using Castle.Windsor;

namespace Luna.Dependency
{
    public class IocManager : IIocManager
    {
        static IocManager()
        {
            Instance = new IocManager();
        }

        public IocManager()
        {
            IocContainer = new WindsorContainer();
            // 把自己注册进容器
            IocContainer.Register(
                Component.For<IocManager, IIocManager>()
                    .UsingFactoryMethod(() => this)
            );
        }

        public static IocManager Instance { get; }

        public IWindsorContainer IocContainer { get; }

        public void RegisterAssemblyByConvention(Assembly assembly)
        {
            IocContainer.Register(
                Classes.FromAssemblyInThisApplication(assembly)
                    .IncludeNonPublicTypes()
                    .BasedOn<ITransientDependency>()
                    .If(type => !type.GetTypeInfo().IsGenericTypeDefinition)
                    .WithServiceAllInterfaces()
                    .WithService.Self()
                    .WithService.DefaultInterfaces()
                    .LifestyleTransient()
            );

            IocContainer.Register(
                Classes.FromAssemblyInThisApplication(assembly)
                    .IncludeNonPublicTypes()
                    .BasedOn<ISingletonDependency>()
                    .If(type => !type.GetTypeInfo().IsGenericTypeDefinition)
                    .WithServiceAllInterfaces()
                    .WithService.Self()
                    .WithService.DefaultInterfaces()
                    .LifestyleSingleton()
            );
        }

        public T Resolve<T>()
        {
            return IocContainer.Resolve<T>();
        }

        public T[] ResolveAll<T>()
        {
            return IocContainer.ResolveAll<T>();
        }

        public bool IsRegistered<T>()
        {
            return IocContainer.Kernel.HasComponent(typeof(T));
        }

        public void Dispose()
        {
            IocContainer?.Dispose();
        }
    }
}