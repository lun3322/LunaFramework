using System;
using System.Collections.Generic;
using System.Text;

namespace Luna.Dependency
{
    public interface ILunaConfiguration : ISingletonDependency
    {
        void Initialize();

        void Setup();
    }
}
