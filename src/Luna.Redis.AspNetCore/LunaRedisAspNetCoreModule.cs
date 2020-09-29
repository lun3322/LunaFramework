using System;
using Luna.Dependency;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis.Extensions.System.Text.Json;

namespace Luna.Redis.AspNetCore
{
    [Dependency(
        typeof(LunaCoreModule)
    )]
    public class LunaRedisAspNetCoreModule : LunaModule
    {
        public override void ConfigureServices(IServiceCollection services)
        {
            if (LunaStarterOptionExtension.Config == null)
            {
                throw new ArgumentException("Please configuration RedisConfiguration");
            }

            services.AddStackExchangeRedisExtensions<SystemTextJsonSerializer>(LunaStarterOptionExtension.Config);
        }
    }
}