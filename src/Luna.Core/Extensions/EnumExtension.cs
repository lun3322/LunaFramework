using System;
using System.ComponentModel;
using System.Linq;

namespace Luna.Extensions
{
    public static class EnumExtension
    {
        public static string GetDescription(this Enum @this)
        {
            var name = @this.ToString();
            var fieldInfo = @this.GetType().GetField(name);

            if (fieldInfo == null) return name;

            var attributes = fieldInfo.GetCustomAttributes(typeof(DescriptionAttribute), false);
            if (!attributes.Any()) return name;

            var first = attributes.First() as DescriptionAttribute;
            return first?.Description;
        }
    }
}