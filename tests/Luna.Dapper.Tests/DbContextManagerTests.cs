using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Moq;
using Xunit;

namespace Luna.Dapper.Tests
{
    public class DbContextManagerTests
    {
        [Fact]
        public void ContainerShouldContainManagerWhenAddLuna()
        {
            var hostBuilder = new HostBuilder();
            hostBuilder.ConfigureWebHostDefaults(m => { m.ConfigureServices(services => { LunaStarter.StartUp<LunaDapperTestsModule>(services); }); });
            var build = hostBuilder.Build();

            var provider = build.Services;
            var contextManager = provider.GetService<ILunaDbContextManager>();
            contextManager.Should().NotBeNull();
        }

        [Fact]
        public void GetConnectionStringIsAJoke()
        {
            const string name = "name";
            const string returnValue = "returnValue";

            var mock = new Mock<ILunaDbContextManager>();
            mock.Setup(m => m.GetConnectionString(name))
                .Returns(returnValue)
                .Verifiable();

            var connectionString = mock.Object.GetConnectionString(name);
            connectionString.Should().Be(returnValue);

            mock.Verify();
        }
    }
}