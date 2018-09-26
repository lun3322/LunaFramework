using System;
using System.Collections.Generic;
using System.Text;
using Castle.Windsor;

namespace Luna.Dependency
{
    internal class IocManager
    {
        public static WindsorContainer Container { get; set; }
    }
}
