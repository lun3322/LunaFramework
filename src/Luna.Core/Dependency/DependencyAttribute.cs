using System;
using System.Collections.Generic;
using System.Linq;

namespace Luna.Dependency
{
    public class DependencyAttribute : Attribute
    {
        public DependencyAttribute(params Type[] types)
        {
            Types = types.ToList();
        }

        public List<Type> Types { get; set; }
    }
}