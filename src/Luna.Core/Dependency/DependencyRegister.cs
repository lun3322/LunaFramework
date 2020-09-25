using System;
using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;

namespace Luna.Dependency
{
    public class DependencyRegister
    {
        private readonly IServiceCollection _service;

        public DependencyRegister(IServiceCollection service)
        {
            _service = service;
        }

        public void RegisterModules(List<Type> moduleList)
        {
            foreach (var module in moduleList)
            {
                var dependencyTypes = TypeFinder.FindDependencyTypesInAssembly(module.Assembly);
                RegisterTypes(dependencyTypes);
            }
        }

        private void RegisterTypes(List<Type> dependencyTypes)
        {
            foreach (var dependencyType in dependencyTypes)
            {
                if (typeof(ISingletonDependency).IsAssignableFrom(dependencyType))
                {
                    AddSingleton(dependencyType);
                }

                if (typeof(IScopedDependency).IsAssignableFrom(dependencyType))
                {
                    AddScoped(dependencyType);
                }

                if (typeof(ITransientDependency).IsAssignableFrom(dependencyType))
                {
                    AddTransient(dependencyType);
                }
            }
        }

        private void AddTransient(Type dependencyType)
        {
            _service.AddTransient(dependencyType);
            foreach (var @interface in dependencyType.GetInterfaces())
            {
                _service.AddTransient(@interface, dependencyType);
            }
        }

        private void AddScoped(Type dependencyType)
        {
            _service.AddScoped(dependencyType);
            foreach (var @interface in dependencyType.GetInterfaces())
            {
                _service.AddScoped(@interface, dependencyType);
            }
        }

        private void AddSingleton(Type dependencyType)
        {
            _service.AddSingleton(dependencyType);
            foreach (var @interface in dependencyType.GetInterfaces())
            {
                _service.AddSingleton(@interface, dependencyType);
            }
        }
    }
}