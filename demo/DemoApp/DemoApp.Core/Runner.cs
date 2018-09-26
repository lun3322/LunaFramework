using System;
using System.Collections.Generic;
using System.Text;
using DemoApp.Service;
using Luna;

namespace DemoApp.Core
{
    public class Runner : LunaRunnerBase
    {
        private readonly IDemoService _demoService;

        public Runner(IDemoService demoService)
        {
            _demoService = demoService;
        }

        public override void Run()
        {
            var message = _demoService.GetMessage();
            Logger.Info($"获取到信息: {message}");
        }
    }
}
