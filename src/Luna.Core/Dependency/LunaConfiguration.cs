using System;
using System.Collections.Generic;
using System.Text;
using Castle.Core.Logging;

namespace Luna.Dependency
{
    public abstract class LunaConfiguration : ILunaConfiguration
    {
        public ILogger Logger { get; set; }
        protected LunaConfiguration()
        {
            Logger = NullLogger.Instance;
        }

        public abstract void Initialize();

        public abstract void Setup();
    }
}
