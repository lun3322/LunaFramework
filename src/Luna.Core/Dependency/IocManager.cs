﻿using Castle.MicroKernel.Registration;
using Castle.Windsor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

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

            AllAssembly = new HashSet<Assembly>();
            AllTypes = new HashSet<Type>();
        }

        public static IocManager Instance { get; }

        public IWindsorContainer IocContainer { get; }
        public HashSet<Assembly> AllAssembly { get; internal set; }
        public HashSet<Type> AllTypes { get; internal set; }

        public void RegisterAssemblyByConvention(Assembly assembly)
        {
            FindAllTypes(assembly);

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

        private void FindAllTypes(Assembly assembly)
        {
            var appName = assembly.FullName.Split('.')[0];

            var assemblyList = new List<Assembly> {assembly};
            var referencedAssemblies = assembly.GetReferencedAssemblies()
                .Where(m => m.FullName.StartsWith(appName))
                .ToList();
            foreach (var name in referencedAssemblies)
            {
                assemblyList.Add(Assembly.Load(name));
            }

            assemblyList.ForEach(m =>
            {
                AllAssembly.Add(m);
                var types = m.GetTypes().Where(x => !x.IsGenericType).ToList();
                types.ForEach(x => AllTypes.Add(x));
            });
        }
    }
}