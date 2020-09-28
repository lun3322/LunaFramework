using System.Threading.Tasks;

namespace Sample.Core.SampleService
{
    public interface IRedisService
    {
        Task<string> GetOrAddMessageInRedis();
    }
}