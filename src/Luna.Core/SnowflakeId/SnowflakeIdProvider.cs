using System;
using System.Collections.Generic;
using System.Text;
using Luna.Dependency;

namespace Luna.SnowflakeId
{
    public class SnowflakeIdProvider : ISnowflakeIdProvider
    {
        /// <summary>
        /// 获取 worker
        /// </summary>
        /// <returns></returns>
        public SnowflakeIdWorker GetWorker()
        {
            return GetWorker(1, 1);
        }

        /// <summary>
        /// 获取 worker
        /// </summary>
        /// <returns></returns>
        public SnowflakeIdWorker GetWorker(long workerId, long datacenterId)
        {
            return new SnowflakeIdWorker(workerId, datacenterId);
        }
    }
}