using Luna.Application;
using Luna.Dapper;

namespace Sample.Core.SampleService
{
    public class SampleService : ISampleService, IBaseSampleService
    {
        private readonly ILunaDbContextManager _dbContextManager;

        public SampleService(ILunaDbContextManager dbContextManager)
        {
            _dbContextManager = dbContextManager;
        }

        public string GetMessage()
        {
            var connectionString = _dbContextManager.GetConnectionString("default") ?? "test default";
            return $"hello world {connectionString}";
        }
    }
}