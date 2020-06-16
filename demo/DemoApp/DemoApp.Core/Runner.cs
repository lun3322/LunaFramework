using Castle.Core.Logging;
using DemoApp.Service;
using Luna.Dependency;
using Luna.SnowflakeId;

namespace DemoApp.Core
{
    public class Runner : ITransientDependency
    {
        public ILogger Logger { get; set; }

        private readonly IDemoService _demoService;
        private readonly ISnowflakeIdProvider _snowflakeIdProvider;

        public Runner(IDemoService demoService, ISnowflakeIdProvider snowflakeIdProvider)
        {
            _demoService = demoService;
            _snowflakeIdProvider = snowflakeIdProvider;
        }

        public void Run()
        {
            var message = _demoService.GetMessage();
            Logger.Info($"获取到信息: {message}");
            var worker = _snowflakeIdProvider.GetWorker();
            Logger.Info($"newid {worker.NextId()}");
            Logger.Info($"newid {worker.NextId()}");
        }
    }
}