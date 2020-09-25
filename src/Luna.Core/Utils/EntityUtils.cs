using System;
using Luna.Extensions;
using Luna.Repository;

namespace Luna.Utils
{
    public static class EntityUtils
    {
        public static Type GetPrimaryKeyType(Type type)
        {
            if (!type.IsImplementedGeneric(typeof(IEntity<>))) throw new ArgumentException($"{nameof(type)}参数未实现IEntity<>");

            var propertyInfo = type.GetProperty("Id");
            return propertyInfo.PropertyType;
        }
    }
}