using Luna.Dependency;

namespace Luna.SnowflakeId
{
    public interface ISnowflakeIdProvider : ISingletonDependency
    {
        /// <summary>
        ///     获取 worker
        /// </summary>
        /// <returns></returns>
        SnowflakeIdWorker GetWorker();

        /// <summary>
        ///     获取 worker
        /// </summary>
        /// <returns></returns>
        SnowflakeIdWorker GetWorker(long workerId, long datacenterId);
    }
}