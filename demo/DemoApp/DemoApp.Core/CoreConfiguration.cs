using System;
using System.Collections.Generic;
using System.Text;
using Luna.Dependency;

namespace DemoApp.Core
{
    public class CoreConfiguration : LunaConfiguration
    {
        public override void Initialize()
        {
            Logger.Info("CoreConfiguration Initialize");
        }

        public override void Setup()
        {
            Logger.Info("CoreConfiguration Setup");
        }
    }
}
