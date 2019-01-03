using Luna.Dependency;
using Luna.Extensions;
using Luna.Repository;
using Luna.Utils;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Luna.Dapper.Repository
{
    public static class RepositoryRegister
    {
        public static void Initialize(IIocManager iocManager)
        {
            var dbContexts = GetAllDbContextType(iocManager);
            var allEntityType = GetAllEntityType(iocManager);
            foreach (var dbContext in dbContexts)
            {
                RegisterRepository(iocManager, dbContext, allEntityType);
            }
        }

        private static List<Type> GetAllDbContextType(IIocManager iocManager)
        {
            return iocManager.AllTypes.Where(m => typeof(ILunaDbContext).IsAssignableFrom(m))
                .Where(m => !m.IsAbstract)
                .ToList();
        }

        private static List<Type> GetAllEntityType(IIocManager iocManager)
        {
            var entityBaseType = typeof(IEntity<>);
            return iocManager.AllTypes
                .Where(m => m != entityBaseType)
                .Where(m => m.IsImplementedGeneric(entityBaseType))
                .ToList();
        }

        private static void RegisterRepository(IIocManager iocManager, Type dbContextType, List<Type> entityTypes)
        {
            var properties = dbContextType.GetProperties()
                .Where(m => entityTypes.Contains(m.PropertyType))
                .ToList();

            properties.ForEach(m =>
            {
                var primaryKeyType = EntityUtils.GetPrimaryKeyType(m.PropertyType);
                Type implType, interfaceType;
                if (typeof(int) == primaryKeyType)
                {
                    implType = typeof(DapperRepositoryBase<,>)
                        .MakeGenericType(m.DeclaringType, m.PropertyType);
                    interfaceType = typeof(IRepository<>)
                        .MakeGenericType(m.PropertyType);
                }
                else
                {
                    implType = typeof(DapperRepositoryBase<,,>)
                        .MakeGenericType(m.DeclaringType, m.PropertyType, primaryKeyType);
                    interfaceType = typeof(IRepository<,>)
                        .MakeGenericType(m.PropertyType, primaryKeyType);
                }
                iocManager.RegisterTypeTransient(interfaceType, implType);
            });
        }
    }
}
