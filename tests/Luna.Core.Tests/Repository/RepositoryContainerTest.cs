using Luna.Repository;
using Shouldly;
using Xunit;

namespace Luna.Core.Tests.Repository
{
    public class RepositoryContainerTest : TestBase
    {
        [Fact]
        public void Repository_Is_Registered_Test()
        {
            var repository1 = IocManager.Resolve<IRepository<TestEntity>>();
            var repository2 = IocManager.Resolve<IRepository<TestEntity1, long>>();

            repository1.ShouldNotBeNull();
            repository2.ShouldNotBeNull();

            repository1.ShouldBeOfType(typeof(TestRepository<TestEntity>));
            repository2.ShouldBeOfType(typeof(TestRepository<TestEntity1, long>));
        }

        [Fact]
        public void Repository_Registered_Test()
        {
            var hasComponent = IocManager.IocContainer.Kernel.HasComponent(typeof(IRepository<>));

            hasComponent.ShouldBe(true);
        }

        [Fact]
        public void Repository_Type_Test()
        {
            var isAssignableFrom = typeof(IRepository).IsAssignableFrom(typeof(TestRepository<>));

            isAssignableFrom.ShouldBeTrue();
        }
    }

    public class TestEntity : IEntity<int>
    {
        public int Id { get; set; }
    }

    public class TestEntity1 : IEntity<long>
    {
        public long Id { get; set; }
    }
}