using Castle.MicroKernel.Registration;
using Luna.Dapper.Repository;
using Luna.Repository;
using Shouldly;
using System;
using System.Linq;
using System.Reflection;
using Xunit;

namespace Luna.Dapper.Tests.Repository
{
    public class LunaDbContextTests : TestBase
    {
        [Fact]
        public void LunaDbContext_IsExist_Test()
        {
            var dbContexts = IocManager.AllTypes.Where(m => typeof(ILunaDbContext).IsAssignableFrom(m))
                .Where(m => !m.IsAbstract)
                .ToList();

            dbContexts.Contains(typeof(LunaDapperTestDbContext)).ShouldBeTrue();
        }

        [Fact]
        public void DbContext_Property_Test()
        {
            var dbContexts = IocManager.AllTypes.Where(m => typeof(ILunaDbContext).IsAssignableFrom(m))
                .Where(m => !m.IsAbstract)
                .ToList();
            var testDbContext = dbContexts.FirstOrDefault(m => m == typeof(LunaDapperTestDbContext));
            testDbContext.ShouldNotBeNull();

            var entityBaseType = typeof(IEntity);
            var entityTypes = IocManager.AllTypes
                .Where(m => m != entityBaseType)
                .Where(m => entityBaseType.IsAssignableFrom(m)).ToList();
            entityTypes.Count.ShouldBeGreaterThan(0);

            var properties = testDbContext.GetProperties()
                .Where(m => entityTypes.Contains(m.PropertyType))
                .ToList();

            properties.Count.ShouldBeGreaterThan(0);
            properties.FirstOrDefault(m => m.PropertyType == typeof(TestEntity)).ShouldNotBeNull();
            properties.FirstOrDefault(m => m.PropertyType == typeof(TestEntityTwo)).ShouldNotBeNull();

            IocManager.IsRegistered<IRepository<TestEntity>>().ShouldBeFalse();
            IocManager.IsRegistered<IRepository<TestEntityTwo>>().ShouldBeFalse();

            properties.ForEach(m =>
            {
                var genericType = typeof(DapperRepositoryBase<>)
                    .MakeGenericType(m.PropertyType);
                var instance = Activator.CreateInstance(genericType, m.DeclaringType);

                IocManager.IocContainer.Register(
                    Component.For(typeof(IRepository<>))
                        .Instance(instance)
                        .LifestyleTransient()
                    );
            });

            IocManager.IsRegistered<IRepository<TestEntity>>().ShouldBeTrue();
            IocManager.IsRegistered<IRepository<TestEntityTwo>>().ShouldBeTrue();
        }
    }
}
