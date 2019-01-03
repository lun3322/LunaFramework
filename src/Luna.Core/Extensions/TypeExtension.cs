using System;
using System.Linq;

namespace Luna.Extensions
{
    public static class TypeExtension
    {
        public static bool IsImplementedGeneric(this Type @this, Type genericType)
        {
            return @this.GetInterfaces()
                .Where(m => m.IsGenericType)
                .Any(m => genericType == m.GetGenericTypeDefinition());
        }
    }
}
