using System;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Xunit;

namespace Luna.MongoDb.Tests
{
    public class MongoDbClientManagerTests
    {
        [Fact]
        public void ContainerShouldContainIMongoDbClientManagerWhenAddLuna()
        {
            var hostBuilder = new HostBuilder();
            hostBuilder.ConfigureWebHostDefaults(m => { m.ConfigureServices(services => { LunaStarter.StartUp<LunaMongoDbTestsModule>(services); }); });
            var build = hostBuilder.Build();

            var provider = build.Services;
            var contextManager = provider.GetService<IMongoDbClientManager>();
            contextManager.Should().NotBeNull();
        }
    }
}