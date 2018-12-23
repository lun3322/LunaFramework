using Shouldly;
using Xunit;

namespace Luna.Repository.Dapper.Tests.Repository.Dapper
{
    public class DapperRepositoryTest : TestBase
    {
        private readonly IRepository<TestEntity> _repository;

        public DapperRepositoryTest()
        {
            _repository = IocManager.Resolve<IRepository<TestEntity>>();
        }

        [Fact]
        public void TestEntity_Insert_Test()
        {
            var entity = new TestEntity
            {
                Name = "测试名称"
            };

            var id = _repository.Insert(entity);

            id.ShouldBeGreaterThan(0);
        }

        [Fact]
        public void TestEntity_InsertAndDelete_Test()
        {
            var entity = new TestEntity
            {
                Name = "测试名称"
            };

            var id = _repository.Insert(entity);
            
            _repository.Delete(id);
            id.ShouldBeGreaterThan(0);
        }
    }
}