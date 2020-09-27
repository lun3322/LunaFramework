using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using TestModule.Core;
using TestModule.Entry;
using Xunit;

namespace Luna.Core.Tests.Dependency
{
    public class LunaStarterTests
    {
        public LunaStarterTests()
        {
            var serviceCollection = new ServiceCollection();
            var starter = LunaStarter.StartUp<TestModuleEntryModule>(serviceCollection);

            _provider = serviceCollection.BuildServiceProvider();
        }

        private readonly ServiceProvider _provider;

        [Fact]
        public void LunaStarterCreateShouldContainSelf()
        {
            var lunaStarter = _provider.GetService<LunaStarter>();
            lunaStarter.Should().NotBeNull();
        }

        [Fact]
        public void LunaStarterProviderShouldContainScopeDependencyThenStarted()
        {
            var dependency = _provider.GetService<ScopeDependency>();

            dependency.Should().NotBeNull();
        }

        [Fact]
        public void LunaStarterProviderShouldContainSingletonDependencyThenStarted()
        {
            var dependency = _provider.GetService<SingletonDependency>();

            dependency.Should().NotBeNull();
        }

        [Fact]
        public void LunaStarterProviderShouldContainTransientDependencyThenStarted()
        {
            var dependency = _provider.GetService<TransientDependency>();

            dependency.Should().NotBeNull();
        }

        [Fact]
        public void ModulesShouldNotRegisteredThenStarted()
        {
            _provider.GetService<TestModuleCoreModule>().Should().BeNull();
            _provider.GetService<TestModuleEntryModule>().Should().BeNull();
            _provider.GetService<LunaCoreModule>().Should().BeNull();
        }

        [Fact]
        public void SubModuleRegisteredDependencyShouldRegistered()
        {
            var dependency = _provider.GetService<SubModuleRegisteredDependency>();

            dependency.Should().NotBeNull();
        }
    }
}