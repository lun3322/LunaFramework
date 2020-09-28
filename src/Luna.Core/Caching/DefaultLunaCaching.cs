using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Memory;

namespace Luna.Caching
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Major Code Smell", "S4457:Parameter validation in \"async\"/\"await\" methods should be wrapped", Justification = "<挂起>")]
    public class DefaultLunaCaching : ILunaCaching
    {
        private readonly HashSet<string> _cacheKeys;
        private readonly object _lockObj = new object();
        private readonly MemoryCache _memoryCache;
        private readonly Random _random;

        public DefaultLunaCaching()
        {
            _memoryCache = new MemoryCache(new MemoryCacheOptions());
            _random = new Random();
            _cacheKeys = new HashSet<string>();
        }

        public void Set<T>(string cacheKey, T cacheValue) where T : class
        {
            if (cacheKey == null) throw new ArgumentNullException(nameof(cacheKey));
            if (cacheValue == null) throw new ArgumentNullException(nameof(cacheValue));

            Set(cacheKey, cacheValue, new TimeSpan());
        }

        public void Set<T>(string cacheKey, T cacheValue, TimeSpan expiration) where T : class
        {
            if (cacheKey == null) throw new ArgumentNullException(nameof(cacheKey));
            if (cacheValue == null) throw new ArgumentNullException(nameof(cacheValue));

            lock (_lockObj)
            {
                _cacheKeys.Add(cacheKey);
                if (expiration == default)
                    _memoryCache.Set(cacheKey, cacheValue);
                else
                    _memoryCache.Set(cacheKey, cacheValue, GetExpirationWithRandom(expiration));
            }
        }

        public async Task SetAsync<T>(string cacheKey, T cacheValue) where T : class
        {
            if (cacheKey == null) throw new ArgumentNullException(nameof(cacheKey));
            if (cacheValue == null) throw new ArgumentNullException(nameof(cacheValue));

            await Task.Run(() => SetAsync(cacheKey, cacheValue, new TimeSpan()));
        }

        public async Task SetAsync<T>(string cacheKey, T cacheValue, TimeSpan expiration)
            where T : class
        {
            if (cacheKey == null) throw new ArgumentNullException(nameof(cacheKey));
            if (cacheValue == null) throw new ArgumentNullException(nameof(cacheValue));

            await Task.Run(() => Set(cacheKey, cacheValue, expiration));
        }

        public T Get<T>(string cacheKey, Func<T> func) where T : class
        {
            if (cacheKey == null) throw new ArgumentNullException(nameof(cacheKey));
            if (func == null) throw new ArgumentNullException(nameof(func));

            return Get(cacheKey, func, new TimeSpan());
        }

        public T Get<T>(string cacheKey, Func<T> func, TimeSpan expiration) where T : class
        {
            if (cacheKey == null) throw new ArgumentNullException(nameof(cacheKey));
            if (func == null) throw new ArgumentNullException(nameof(func));

            var item = _memoryCache.Get<T>(cacheKey);
            if (item != null) return item;
            item = func.Invoke();
            Set(cacheKey, item, expiration);

            return item;
        }

        public T Get<T>(string cacheKey) where T : class
        {
            if (cacheKey == null) throw new ArgumentNullException(nameof(cacheKey));

            return _memoryCache.Get<T>(cacheKey);
        }

        public async Task<T> GetAsync<T>(string cacheKey, Func<Task<T>> func) where T : class
        {
            if (cacheKey == null) throw new ArgumentNullException(nameof(cacheKey));
            if (func == null) throw new ArgumentNullException(nameof(func));

            return await GetAsync(cacheKey, func, new TimeSpan());
        }

        public async Task<T> GetAsync<T>(string cacheKey, Func<Task<T>> func, TimeSpan expiration)
            where T : class
        {
            if (cacheKey == null) throw new ArgumentNullException(nameof(cacheKey));
            if (func == null) throw new ArgumentNullException(nameof(func));

            var item = await Task.FromResult(_memoryCache.Get<T>(cacheKey));
            if (item != null) return item;
            item = await func.Invoke();
            await SetAsync(cacheKey, item, expiration);

            return item;
        }

        public async Task<T> GetAsync<T>(string cacheKey) where T : class
        {
            if (cacheKey == null) throw new ArgumentNullException(nameof(cacheKey));

            return await Task.FromResult(Get<T>(cacheKey));
        }

        public void Remove(string cacheKey)
        {
            if (cacheKey == null) throw new ArgumentNullException(nameof(cacheKey));

            lock (_lockObj)
            {
                _cacheKeys.Remove(cacheKey);
                _memoryCache.Remove(cacheKey);
            }
        }

        public async Task RemoveAsync(string cacheKey)
        {
            if (cacheKey == null) throw new ArgumentNullException(nameof(cacheKey));

            await Task.Run(() => { Remove(cacheKey); });
        }

        public bool Exists(string cacheKey)
        {
            if (cacheKey == null) throw new ArgumentNullException(nameof(cacheKey));

            lock (_lockObj)
            {
                var tryGetValue = _memoryCache.TryGetValue(cacheKey, out _);
                if (!tryGetValue) _cacheKeys.Remove(cacheKey);

                return tryGetValue;
            }
        }

        public async Task<bool> ExistsAsync(string cacheKey)
        {
            if (cacheKey == null) throw new ArgumentNullException(nameof(cacheKey));

            return await Task.FromResult(Exists(cacheKey));
        }

        public void RemoveByPrefix(string prefix)
        {
            if (prefix == null) throw new ArgumentNullException(nameof(prefix));

            List<string> keys;
            lock (_lockObj)
            {
                keys = _cacheKeys.Where(m => m.StartsWith(prefix)).ToList();
            }

            foreach (var key in keys) Remove(key);
        }

        public async Task RemoveByPrefixAsync(string prefix)
        {
            if (prefix == null) throw new ArgumentNullException(nameof(prefix));

            await Task.Run(() => RemoveByPrefix(prefix));
        }

        public void SetAll<T>(IDictionary<string, T> value) where T : class
        {
            if (value == null) throw new ArgumentNullException(nameof(value));

            SetAll(value, new TimeSpan());
        }

        public void SetAll<T>(IDictionary<string, T> value, TimeSpan expiration) where T : class
        {
            if (value == null) throw new ArgumentNullException(nameof(value));

            foreach (var kv in value) Set(kv.Key, kv.Value, expiration);
        }

        public async Task SetAllAsync<T>(IDictionary<string, T> value) where T : class
        {
            if (value == null) throw new ArgumentNullException(nameof(value));

            await Task.Run(() => SetAllAsync(value, new TimeSpan()));
        }

        public async Task SetAllAsync<T>(IDictionary<string, T> value, TimeSpan expiration) where T : class
        {
            if (value == null) throw new ArgumentNullException(nameof(value));

            foreach (var kv in value) await SetAsync(kv.Key, kv.Value, expiration);
        }

        public IDictionary<string, T> GetAll<T>(IEnumerable<string> cacheKeys) where T : class
        {
            if (cacheKeys == null) throw new ArgumentNullException(nameof(cacheKeys));

            var dic = new Dictionary<string, T>();
            foreach (var cacheKey in cacheKeys)
            {
                var item = Get<T>(cacheKey);
                dic.Add(cacheKey, item);
            }

            return dic;
        }

        public async Task<IDictionary<string, T>> GetAllAsync<T>(IEnumerable<string> cacheKeys)
            where T : class
        {
            if (cacheKeys == null) throw new ArgumentNullException(nameof(cacheKeys));

            return await Task.FromResult(GetAll<T>(cacheKeys));
        }

        public IDictionary<string, T> GetByPrefix<T>(string prefix) where T : class
        {
            if (prefix == null) throw new ArgumentNullException(nameof(prefix));

            List<string> keys;
            lock (_lockObj)
            {
                keys = _cacheKeys.Where(m => m.StartsWith(prefix)).ToList();
            }

            var dic = new Dictionary<string, T>();
            foreach (var key in keys) dic.Add(key, Get<T>(key));

            return dic;
        }

        public async Task<IDictionary<string, T>> GetByPrefixAsync<T>(string prefix) where T : class
        {
            if (prefix == null) throw new ArgumentNullException(nameof(prefix));

            return await Task.FromResult(GetByPrefix<T>(prefix));
        }

        public void RemoveAll(IEnumerable<string> cacheKeys)
        {
            if (cacheKeys == null) throw new ArgumentNullException(nameof(cacheKeys));

            foreach (var cacheKey in cacheKeys) Remove(cacheKey);
        }

        public async Task RemoveAllAsync(IEnumerable<string> cacheKeys)
        {
            if (cacheKeys == null) throw new ArgumentNullException(nameof(cacheKeys));

            await Task.Run(() => RemoveAll(cacheKeys));
        }

        public int GetCount(string prefix = "")
        {
            if (string.IsNullOrEmpty(prefix)) return _memoryCache.Count;

            lock (_lockObj)
            {
                var list = _cacheKeys.Where(m => m.StartsWith(prefix)).ToList();

                foreach (var s in list)
                    if (!_memoryCache.TryGetValue(s, out _))
                        _cacheKeys.Remove(s);

                return _cacheKeys.Count(m => m.StartsWith(prefix));
            }
        }

        public int MaxRandomSecond => 60;

        private TimeSpan GetExpirationWithRandom(TimeSpan expiration)
        {
            if (expiration.TotalSeconds < MaxRandomSecond) return expiration;

            var rndSeconds = TimeSpan.FromSeconds(_random.Next(MaxRandomSecond));
            return expiration.Add(rndSeconds);
        }
    }
}