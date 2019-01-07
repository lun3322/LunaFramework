using Luna.Repository;
using Shouldly;
using System.Linq;
using Luna.Dapper.Tests.Domain;
using Luna.Extensions;
using Xunit;

namespace Luna.Dapper.Tests
{
    public class EntityTests : TestBase
    {
        [Fact]
        public void Multi_Entity_Test()
        {
            var entityBaseType = typeof(IEntity<>);
            var entityTypes = IocManager.AllTypes
                .Where(m => m != entityBaseType)
                .Where(m => m.IsImplementedGeneric(entityBaseType))
                .ToList();

            entityTypes.Count.ShouldBe(3);
            entityTypes.Contains(typeof(TestEntity)).ShouldBeTrue();
            entityTypes.Contains(typeof(TestEntityTwo)).ShouldBeTrue();
        }
    }
}
