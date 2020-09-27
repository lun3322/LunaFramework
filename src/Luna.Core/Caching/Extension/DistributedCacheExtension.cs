using System;
using System.Text.Json;
using System.Threading.Tasks;
using Luna.Extensions;
using Microsoft.Extensions.Caching.Distributed;

namespace Luna.Caching.Extension
{
    public static class DistributedCacheExtension
    {
        public static T Get<T>(this IDistributedCache distributedCache, string key)
        {
            var jsonString = distributedCache.GetString(key);
            if (jsonString.IsNotNullOrWhiteSpace())
            {
                return JsonSerializer.Deserialize<T>(jsonString);
            }

            return default(T);
        }

        public static async Task<T> GetAsync<T>(this IDistributedCache distributedCache, string key)
        {
            var jsonString = await distributedCache.GetStringAsync(key);
            if (jsonString.IsNotNullOrWhiteSpace())
            {
                return JsonSerializer.Deserialize<T>(jsonString);
            }

            return default(T);
        }

        public static void Set<T>(this IDistributedCache distributedCache,
            string key,
            T value,
            DistributedCacheEntryOptions options = null)
        {
            var jsonString = JsonSerializer.Serialize(value);
            var opt = options ?? new DistributedCacheEntryOptions();
            distributedCache.SetString(key, jsonString, opt);
        }

        public static Task SetAsync<T>(this IDistributedCache distributedCache,
            string key,
            T value,
            DistributedCacheEntryOptions options = null)
        {
            var jsonString = JsonSerializer.Serialize(value);
            var opt = options ?? new DistributedCacheEntryOptions();
            return distributedCache.SetStringAsync(key, jsonString, opt);
        }

        public static T Get<T>(this IDistributedCache distributedCache, string key, Func<T> func, DistributedCacheEntryOptions options = null)
        {
            var item = distributedCache.Get<T>(key);
            if (item != null)
            {
                return item;
            }

            item = func.Invoke();
            distributedCache.Set(key, item, options);
            return item;
        }

        public static async Task<T> GetAsync<T>(this IDistributedCache distributedCache, string key, Func<Task<T>> func, DistributedCacheEntryOptions options = null)
        {
            var item = await distributedCache.GetAsync<T>(key);
            if (item != null)
            {
                return item;
            }

            item = await func.Invoke();
            await distributedCache.SetAsync(key, item, options);
            return item;
        }
    }
}