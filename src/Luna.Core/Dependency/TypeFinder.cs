using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Luna.TopologicalSorting;

namespace Luna.Dependency
{
    public static class TypeFinder
    {
        public static List<Type> FindDependencyTypesInAssembly(Assembly assembly)
        {
            var types = assembly.GetTypes();
            var dependencyTypes = types.Where(m => m.IsClass)
                .Where(m => !m.IsGenericType)
                .Where(m => !m.IsAbstract)
                .Where(m => typeof(IScopedDependency).IsAssignableFrom(m)
                            || typeof(ISingletonDependency).IsAssignableFrom(m)
                            || typeof(ITransientDependency).IsAssignableFrom(m)
                )
                .ToList();

            return dependencyTypes;
        }

        public static List<Type> FindModulesInAssemblyList(List<Assembly> assemblyList)
        {
            var allModule = new HashSet<Type>();
            foreach (var assembly in assemblyList)
            {
                var modules = FindModuleInAssembly(assembly);
                modules.ForEach(m => allModule.Add(m));
            }

            return ModuleSorting(allModule);
        }

        private static bool IsLunaModule(Type type)
        {
            return type.IsClass &&
                   !type.IsGenericType &&
                   !type.IsAbstract &&
                   typeof(LunaModule).IsAssignableFrom(type);
        }

        private static List<Type> FindModuleInAssembly(Assembly assembly)
        {
            var allTypes = assembly.GetTypes();
            var moduleTypes = allTypes.Where(IsLunaModule)
                .ToList();

            var modules = new HashSet<Type>(moduleTypes);

            foreach (var moduleType in moduleTypes)
            {
                var attributes = moduleType
                    .GetCustomAttributes(typeof(DependencyAttribute), false)
                    .Cast<DependencyAttribute>();

                foreach (var dependencyAttribute in attributes)
                {
                    var dependencyTypes = dependencyAttribute.Types
                        .Where(IsLunaModule)
                        .ToList();
                    dependencyTypes.ForEach(m => modules.Add(m));
                }
            }

            return modules.ToList();
        }

        private static List<Type> ModuleSorting(IEnumerable<Type> allModule)
        {
            var graph = new DependencyGraph<Type>();

            foreach (var module in allModule)
            {
                // ReSharper disable once S1848
                new OrderedProcess<Type>(graph, module);
            }

            foreach (var process in graph.Processes)
            {
                var attributes = process.Value
                    .GetCustomAttributes(typeof(DependencyAttribute), false)
                    .Cast<DependencyAttribute>();

                foreach (var dependencyAttribute in attributes)
                {
                    var processes = graph.Processes
                        .Where(m => dependencyAttribute.Types.Contains(m.Value))
                        .ToList();
                    if (processes.Any())
                    {
                        process.After(processes);
                    }
                }
            }

            var sortedModule = new List<Type>();
            var calculateSort = graph.CalculateSort();
            foreach (OrderedProcess<Type> orderedProcess in calculateSort)
            {
                sortedModule.Add(orderedProcess.Value);
            }

            return sortedModule;
        }
    }
}