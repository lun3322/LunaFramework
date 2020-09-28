using FluentAssertions;
using Luna.Dapper;
using Luna.MongoDb;
using Luna.SnowflakeId;
using Luna.Web.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TestModule.Entry;
using Xunit;

namespace Luna.Web.Tests
{
    public class LunaStarterTests
    {
        public LunaStarterTests()
        {
        }

        [Fact]
        public void ContainerShouldBeContainAllReferencedLunaModuleWhenStartUp()
        {
            var hostBuilder = new HostBuilder();
            hostBuilder.ConfigureWebHostDefaults(m => { m.ConfigureServices(services => { services.AddLuna<TestModuleEntryModule>(); }); });
            var build = hostBuilder.Build();

            var provider = build.Services;

            provider.GetService<ILunaDbContextManager>().Should().NotBeNull();
            provider.GetService<ILunaMongoDbClientManager>().Should().NotBeNull();
            provider.GetService<LunaExceptionFilter>().Should().NotBeNull();
            provider.GetService<ModelVerificationFilter>().Should().NotBeNull();
            provider.GetService<ISnowflakeIdProvider>().Should().NotBeNull();
        }
    }
}