using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Luna.Dependency;

namespace Luna.Caching
{
    public interface ILunaCaching : ISingletonDependency
    {
        /// <summary>
        ///     加入缓存时增加的随机秒数 最大值
        /// </summary>
        int MaxRandomSecond { get; }

        /// <summary>
        ///     设置缓存项
        /// </summary>
        void Set<T>(string cacheKey, T cacheValue) where T : class;

        /// <summary>
        ///     设置缓存项
        /// </summary>
        void Set<T>(string cacheKey, T cacheValue, TimeSpan expiration) where T : class;

        /// <summary>
        ///     设置缓存项
        /// </summary>
        Task SetAsync<T>(string cacheKey, T cacheValue) where T : class;

        /// <summary>
        ///     设置缓存项
        /// </summary>
        Task SetAsync<T>(string cacheKey, T cacheValue, TimeSpan expiration) where T : class;

        /// <summary>
        ///     获取一个缓存,不存在时使用func返回值设置并返回
        /// </summary>
        T Get<T>(string cacheKey, Func<T> func) where T : class;

        /// <summary>
        ///     获取一个缓存,不存在时使用func返回值设置并返回
        /// </summary>
        T Get<T>(string cacheKey, Func<T> func, TimeSpan expiration) where T : class;

        /// <summary>
        ///     获取一个缓存,不存在时使用func返回值设置并返回
        /// </summary>
        Task<T> GetAsync<T>(string cacheKey, Func<Task<T>> func)
            where T : class;

        /// <summary>
        ///     获取一个缓存,不存在时使用func返回值设置并返回
        /// </summary>
        Task<T> GetAsync<T>(string cacheKey, Func<Task<T>> func, TimeSpan expiration)
            where T : class;

        /// <summary>
        ///     获取一个缓存
        /// </summary>
        T Get<T>(string cacheKey) where T : class;

        /// <summary>
        ///     获取一个缓存
        /// </summary>
        Task<T> GetAsync<T>(string cacheKey) where T : class;

        /// <summary>
        ///     删除一个缓存
        /// </summary>
        void Remove(string cacheKey);

        /// <summary>
        ///     删除一个缓存
        /// </summary>
        Task RemoveAsync(string cacheKey);

        /// <summary>
        ///     判断缓存是否存在
        /// </summary>
        bool Exists(string cacheKey);

        /// <summary>
        ///     判断缓存是否存在
        /// </summary>
        Task<bool> ExistsAsync(string cacheKey);

        /// <summary>
        ///     删除指定前缀的缓存
        /// </summary>
        void RemoveByPrefix(string prefix);

        /// <summary>
        ///     删除指定前缀的缓存
        /// </summary>
        Task RemoveByPrefixAsync(string prefix);

        /// <summary>
        ///     设置缓存
        /// </summary>
        void SetAll<T>(IDictionary<string, T> value) where T : class;

        /// <summary>
        ///     设置缓存
        /// </summary>
        void SetAll<T>(IDictionary<string, T> value, TimeSpan expiration) where T : class;

        /// <summary>
        ///     设置缓存
        /// </summary>
        Task SetAllAsync<T>(IDictionary<string, T> value) where T : class;

        /// <summary>
        ///     设置缓存
        /// </summary>
        Task SetAllAsync<T>(IDictionary<string, T> value, TimeSpan expiration) where T : class;

        /// <summary>
        ///     获取多个缓存
        /// </summary>
        IDictionary<string, T> GetAll<T>(IEnumerable<string> cacheKeys) where T : class;

        /// <summary>
        ///     获取多个缓存
        /// </summary>
        Task<IDictionary<string, T>> GetAllAsync<T>(IEnumerable<string> cacheKeys) where T : class;

        /// <summary>
        ///     根据前缀返回多个缓存
        /// </summary>
        IDictionary<string, T> GetByPrefix<T>(string prefix) where T : class;

        /// <summary>
        ///     根据前缀返回多个缓存
        /// </summary>
        Task<IDictionary<string, T>> GetByPrefixAsync<T>(string prefix) where T : class;

        /// <summary>
        ///     删除指定缓存
        /// </summary>
        void RemoveAll(IEnumerable<string> cacheKeys);

        /// <summary>
        ///     删除指定缓存
        /// </summary>
        Task RemoveAllAsync(IEnumerable<string> cacheKeys);

        /// <summary>
        ///     根据缓存key前缀返回个数
        /// </summary>
        int GetCount(string prefix = "");
    }
}