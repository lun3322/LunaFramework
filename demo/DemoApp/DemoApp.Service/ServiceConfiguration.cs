using System;
using System.Collections.Generic;
using System.Text;
using Luna.Dependency;

namespace DemoApp.Service
{
    public class ServiceConfiguration : LunaConfiguration
    {
        public override void Initialize()
        {
            Logger.Info("ServiceConfiguration Initialize");
        }

        public override void Setup()
        {
            Logger.Info("ServiceConfiguration Setup");
        }
    }
}
