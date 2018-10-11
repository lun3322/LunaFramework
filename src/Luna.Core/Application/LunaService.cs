using System;
using System.Collections.Generic;
using System.Text;
using Castle.Core.Logging;
using Castle.Windsor;
using Luna.Caching;
using Luna.Dependency;

namespace Luna.Application
{
    public abstract class LunaService : ILunaService
    {
        public ILogger Logger { protected get; set; }
        public ILunaCaching Cache { get; set; }
        public IIocManager IocManager { get; set; }

        protected LunaService()
        {
            Logger = NullLogger.Instance;
        }
    }
}
