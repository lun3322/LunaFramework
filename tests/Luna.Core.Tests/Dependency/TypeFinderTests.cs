using System.Collections.Generic;
using System.Reflection;
using FluentAssertions;
using Luna.Dependency;
using TestModule.Core;
using TestModule.Core.Service;
using TestModule.Entry;
using Xunit;

namespace Luna.Core.Tests.Dependency
{
    public class TypeFinderTests
    {
        private readonly List<Assembly> _assemblyList;

        public TypeFinderTests()
        {
            _assemblyList = new List<Assembly>
            {
                typeof(TestModuleCoreModule).Assembly,
                typeof(TestModuleEntryModule).Assembly
            };
        }

        [Fact]
        public void MustFound2ModulesInTestModule()
        {
            var moduleList = TypeFinder.FindModulesInAssemblyList(_assemblyList);

            moduleList.Contains(typeof(TestModuleCoreModule)).Should().BeTrue();
            moduleList.Contains(typeof(TestModuleEntryModule)).Should().BeTrue();
        }

        [Fact]
        public void FindDependencyTypesInAssemblyShouldReturnTargetTypes()
        {
            var assembly = typeof(TestModuleCoreModule).Assembly;

            var types = TypeFinder.FindDependencyTypesInAssembly(assembly);

            types.Contains(typeof(TransientDependency)).Should().BeTrue();
            types.Contains(typeof(ScopeDependency)).Should().BeTrue();
            types.Contains(typeof(DemoService)).Should().BeTrue();
        }
    }
}