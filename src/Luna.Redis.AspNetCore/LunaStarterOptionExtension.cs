using StackExchange.Redis.Extensions.Core.Configuration;

namespace Luna.Redis.AspNetCore
{
    public static class LunaStarterOptionExtension
    {
        public static RedisConfiguration Config { get; set; }

        public static RedisConfiguration RedisConfig(this LunaStarterOption @this)
        {
            return Config ??= new RedisConfiguration();
        }
    }
}