using System;
using System.Collections.Generic;
using FluentAssertions;
using Luna.Dependency;
using Microsoft.Extensions.DependencyInjection;
using TestModule.Core;
using TestModule.Core.Service;
using TestModule.Entry;
using Xunit;

namespace Luna.Core.Tests.Dependency
{
    public class DependencyRegisterTests
    {
        private readonly ServiceProvider _serviceProvider;

        public DependencyRegisterTests()
        {
            var service = new ServiceCollection();
            var dependencyRegister = new DependencyRegister(service);
            var moduleList = new List<Type>
            {
                typeof(TestModuleCoreModule),
                typeof(TestModuleEntryModule)
            };
            dependencyRegister.RegisterModules(moduleList);
            _serviceProvider = service.BuildServiceProvider();
        }

        [Fact]
        public void RegisterModulesShouldContainScopeDependency()
        {
            var dependency = _serviceProvider.GetService<ScopeDependency>();

            dependency.Should().NotBeNull();
        }

        [Fact]
        public void RegisterModulesShouldContainSingletonDependency()
        {
            var dependency = _serviceProvider.GetService<SingletonDependency>();

            dependency.Should().NotBeNull();
        }

        [Fact]
        public void RegisterModulesShouldContainTransientDependency()
        {
            var dependency = _serviceProvider.GetService<TransientDependency>();

            dependency.Should().NotBeNull();
        }

        [Fact]
        public void LunaStarterProviderShouldContainDemoServiceThenStarted()
        {
            var dependency = _serviceProvider.GetService<IDemoService>();

            dependency.Should().NotBeNull();
        }

        [Fact]
        public void CallDemoServiceMethodShouldReturnZeroWhenStarted()
        {
            var demoService = _serviceProvider.GetService<IDemoService>();

            demoService.Should().NotBeNull();
            demoService.GetCode().Should().Be(0);
        }
    }
}