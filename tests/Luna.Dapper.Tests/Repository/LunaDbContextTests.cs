using Castle.MicroKernel.Registration;
using Luna.Dapper.Repository;
using Luna.Dapper.Tests.Entities;
using Luna.Extensions;
using Luna.Repository;
using Luna.Utils;
using Shouldly;
using System;
using System.Linq;
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
            IocManager.IsRegistered<IRepository<TestEntity>>().ShouldBeTrue();
            IocManager.IsRegistered<IRepository<TestEntityTwo>>().ShouldBeTrue();
            IocManager.IsRegistered<IRepository<TestEntityThree, long>>().ShouldBeTrue();
        }

        [Fact]
        public void GenericType_Test()
        {
            var interfactType = typeof(IEntity<>);
            var entityType = typeof(TestEntity);

            entityType.IsImplementedGeneric(interfactType).ShouldBeTrue();
        }

        [Fact]
        public void PrimaryKeyType_Test()
        {
            var type = typeof(TestEntity);

            EntityUtils.GetPrimaryKeyType(type).ShouldBe(typeof(int));
        }

        [Fact]
        public void PrimaryKeyType_LongType_Test()
        {
            var type = typeof(TestEntityThree);

            EntityUtils.GetPrimaryKeyType(type).ShouldBe(typeof(long));
        }
    }
}
