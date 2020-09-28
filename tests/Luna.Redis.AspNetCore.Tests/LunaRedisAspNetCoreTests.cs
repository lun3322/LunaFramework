using System;
using FluentAssertions;
using Luna.Web;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using StackExchange.Redis.Extensions.Core.Configuration;
using Xunit;

namespace Luna.Redis.AspNetCore.Tests
{
    public class LunaRedisAspNetCoreTests
    {
        [Fact]
        public void UnSetLunaOptionRedisConfigurationShouldThrowExceptionTest()
        {
            try
            {
                var hostBuilder = new HostBuilder();
                hostBuilder.ConfigureWebHostDefaults(m => { m.ConfigureServices(services => { services.AddLuna<LunaRedisAspNetCoreTestsModule>(); }); });
                hostBuilder.Build();
            }
            catch (ArgumentException ex)
            {
                ex.Should().NotBeNull();
                ex.Message.Should().Be("Please configuration RedisConfiguration");
            }
        }

        [Fact]
        public void SetLunaOptionRedisConfigurationTest()
        {
            const string connectionString = "localhost:16379";
            const int database = 1;

            var hostBuilder = new HostBuilder();
            hostBuilder.ConfigureWebHostDefaults(m =>
            {
                m.ConfigureServices(services =>
                {
                    services.AddLuna<LunaRedisAspNetCoreTestsModule>(opt =>
                    {
                        opt.RedisConfig().ConnectionString = connectionString;
                        opt.RedisConfig().Database = database;
                    });
                });
            });
            var build = hostBuilder.Build();

            var serviceProvider = build.Services;

            var redisConfiguration = serviceProvider.GetService<RedisConfiguration>();
            redisConfiguration.ConnectionString.Should().Be(connectionString);
            redisConfiguration.Database.Should().Be(database);
        }
    }
}