using System;
using System.Collections.Generic;
using System.Text;
using Castle.Core.Logging;
using DemoApp.Service;
using Luna;
using Luna.Dependency;

namespace DemoApp.Core
{
    public class Runner : IRunner, ITransientDependency
    {
        public ILogger Logger { get; set; }

        private readonly IDemoService _demoService;

        public Runner(IDemoService demoService)
        {
            _demoService = demoService;
        }

        public void Run()
        {
            var message = _demoService.GetMessage();
            Logger.Info($"获取到信息: {message}");
        }
    }
}
