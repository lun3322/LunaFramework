using System.Text.Json;
using System.Threading.Tasks;
using StackExchange.Redis.Extensions.Core.Abstractions;

namespace Sample.Core.SampleService
{
    public class RedisService : IRedisService, IBaseSampleService
    {
        private readonly IRedisCacheClient _redisCacheClient;
        private readonly IRedisDatabase _redisDatabase;

        public RedisService(IRedisCacheClient redisCacheClient, IRedisDatabase redisDatabase)
        {
            _redisCacheClient = redisCacheClient;
            _redisDatabase = redisDatabase;
        }

        public async Task<string> GetOrAddMessageInRedis()
        {
            const string key = "testRedisKey";
            const string value = "测试一段内容";
            var model = new RedisCacheModel {Message = value};
            var msg = await _redisDatabase.GetAsync<RedisCacheModel>(key);
            if (msg == null)
            {
                await _redisDatabase.AddAsync(key, model);
                msg = model;
            }

            await _redisCacheClient.Db1.SetAddAsync("testk1", model);
            _redisCacheClient.Db2.Database.StringSet("testk2", JsonSerializer.Serialize(model));

            return msg.Message;
        }
    }

    public class RedisCacheModel
    {
        public string Message { get; set; }
    }
}