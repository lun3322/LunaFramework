using System;
using System.Collections.Generic;
using System.Text;
using Castle.Core.Logging;
using Castle.Windsor;
using Luna.Caching;
using Luna.Dependency;

namespace Luna
{
    public abstract class LunaRunnerBase : IRunner
    {
        public ILogger Logger { get; set; }
        public ILunaCaching Cache { get; set; }
        public readonly WindsorContainer Container;

        protected LunaRunnerBase()
        {
            Logger = NullLogger.Instance;
            Container = IocManager.Container;
        }

        public abstract void Run();

        public virtual void Stop()
        {

        }

        public virtual void Init()
        {

        }
    }
}
