﻿using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using Castle.MicroKernel.Registration;
using Castle.Windsor;

namespace Luna.Dependency
{
    public class IocManager : IIocManager
    {
        public static IocManager Instance { get; private set; }

        public IWindsorContainer IocContainer { get; private set; }

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

        public void RegisterAssemblyByConvention(Assembly assembly)
        {
            IocContainer.Register(
                Classes.FromAssemblyInThisApplication(assembly)
                    .IncludeNonPublicTypes()
                    .BasedOn<ITransientDependency>()
                    .WithServiceAllInterfaces()
                    .If(type => !type.IsGenericTypeDefinition)
                    .WithService.Self()
                    .WithService.DefaultInterfaces()
                    .LifestyleTransient()
            );

            IocContainer.Register(
                Classes.FromAssemblyInThisApplication(assembly)
                    .IncludeNonPublicTypes()
                    .BasedOn<ISingletonDependency>()
                    .If(type => !type.IsGenericTypeDefinition)
                    .WithService.Self()
                    .WithService.DefaultInterfaces()
                    .LifestyleSingleton()
            );
        }

        public T Resolve<T>()
        {
            return IocContainer.Resolve<T>();
        }

        public void Dispose()
        {
            IocContainer?.Dispose();
        }
    }
}
