using System;
using System.Collections.Generic;
using System.Linq;

namespace Luna.Dependency
{
    public static class LunaConfigurationRegister
    {
        public static void Initialize(IIocManager iocManager)
        {
            var types = GetAllLunaConfigurationType(iocManager);
            foreach (var type in types)
            {
                iocManager.RegisterTypeSingleton(typeof(ILunaConfiguration), type);
            }
        }

        private static List<Type> GetAllLunaConfigurationType(IIocManager iocManager)
        {
            return iocManager.AllTypes
                .Where(m => typeof(ILunaConfiguration).IsAssignableFrom(m))
                .Where(m => !m.IsAbstract)
                .ToList();
        }
    }
}
