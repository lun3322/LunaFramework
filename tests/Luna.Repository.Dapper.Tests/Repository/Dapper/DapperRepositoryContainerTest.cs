using Shouldly;
using Xunit;

namespace Luna.Repository.Dapper.Tests.Repository.Dapper
{
    public class DapperRepositoryContainerTest : TestBase
    {
        [Fact]
        public void Repository_Is_Registered()
        {
            var repository1 = IocManager.Resolve<IRepository<TestEntity>>();
            var repository2 = IocManager.Resolve<IRepository<TestEntity1, long>>();

            repository1.ShouldNotBeNull();
            repository2.ShouldNotBeNull();

            repository1.ShouldBeOfType(typeof(DapperRepository<TestEntity>));
            repository2.ShouldBeOfType(typeof(DapperRepository<TestEntity1, long>));
        }
    }
}