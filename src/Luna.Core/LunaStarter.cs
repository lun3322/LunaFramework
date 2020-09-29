using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Luna.Dependency;
using Microsoft.Extensions.DependencyInjection;

namespace Luna
{
    public class LunaStarter
    {
        private readonly IServiceCollection _serviceCollection;

        private LunaStarter(IServiceCollection services, Type entryType, LunaStarterOption option)
        {
            _serviceCollection = services;

            var mainDomain = entryType.FullName.Split(".").First();

            var allAssembly = GetReferencedAssemblies(entryType, mainDomain);

            var allModule = TypeFinder.FindModulesInAssemblyList(allAssembly);

            var dependencyRegister = new DependencyRegister(_serviceCollection);
            dependencyRegister.RegisterModules(allModule);

            InitializeModule(allModule);
        }

        public static LunaStarter StartUp<T>(IServiceCollection services, Action<LunaStarterOption> action = null)
            where T : LunaModule
        {
            var option = new LunaStarterOption();
            action?.Invoke(option);

            var start = new LunaStarter(services, typeof(T), option);
            services.AddSingleton(typeof(LunaStarter), start);
            return start;
        }

        private static List<Assembly> GetReferencedAssemblies(Type entryType, string mainDomain)
        {
            var allAssembly = new List<Assembly> {entryType.Assembly};
            var referencedAssemblies = entryType.Assembly
                .GetReferencedAssemblies()
                .Where(m => m.FullName.StartsWith(mainDomain) || m.FullName.StartsWith(nameof(Luna)));
            allAssembly.AddRange(referencedAssemblies.Select(Assembly.Load));
            return allAssembly;
        }

        private void InitializeModule(List<Type> moduleList)
        {
            foreach (var module in moduleList)
            {
                var instance = Activator.CreateInstance(module) as LunaModule;
                if (instance == null)
                {
                    throw new ArgumentNullException($"Can't instantiation type {module.FullName}");
                }

                instance.ConfigureServices(_serviceCollection);
            }
        }
    }
}