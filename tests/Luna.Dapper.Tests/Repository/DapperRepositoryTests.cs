using Dapper;
using Luna.Repository;
using Shouldly;
using System;
using System.Threading.Tasks;
using Luna.Dapper.Tests.Domain;
using Xunit;
using Xunit.Abstractions;

namespace Luna.Dapper.Tests.Repository
{
    public class DapperRepositoryTests : TestBase
    {
        private readonly ITestOutputHelper _output;
        public DapperRepositoryTests(ITestOutputHelper output)
        {
            _output = output;
        }

        [Fact]
        public void TestEntityRepository_Is_Registered_Test()
        {
            IocManager.IsRegistered<ITestEntityRepository>().ShouldBeTrue();
            IocManager.IsRegistered<ITestEntityTwoRepository>().ShouldBeTrue();
        }

        [Fact]
        public void TestEntityRepository_Test()
        {
            var repository = IocManager.Resolve<ITestEntityRepository>();
            var entity = new TestEntity
            {
                Name = Guid.NewGuid().ToString(),
                Age = 33
            };
            var firstInsert = repository.Insert(entity);
            var secondInsert = repository.Insert(entity);

            _output.WriteLine(firstInsert.ToString());
            _output.WriteLine(secondInsert.ToString());

            firstInsert.ShouldBeGreaterThan(0);
            secondInsert.ShouldBeGreaterThan(0);

            entity.Age = 44;
            repository.Update(entity);
            var testEntity = repository.Get(entity.Id);
            testEntity.Age.ShouldBe(44);

            repository.Count().ShouldBe(2);
            repository.Delete(firstInsert);
            repository.Delete(secondInsert);
            repository.Count().ShouldBe(0);
        }

        [Fact]
        public async Task TestEntityTwoRepository_Async_Test()
        {
            var repository = IocManager.Resolve<ITestEntityTwoRepository>();

            var entity = new TestEntityTwo
            {
                Name = Guid.NewGuid().ToString(),
                Age = 33
            };

            var firstId = await repository.InsertAsync(entity);
            var secondId = await repository.InsertAsync(entity);

            _output.WriteLine(firstId.ToString());
            _output.WriteLine(secondId.ToString());

            firstId.ShouldBeGreaterThan(0);
            secondId.ShouldBeGreaterThan(0);

            entity.Age = 44;
            await repository.UpdateAsync(entity);
            var testEntity = await repository.GetAsync(entity.Id);
            testEntity.Age.ShouldBe(44);

            (await repository.CountAsync()).ShouldBe(2);
            await repository.DeleteAsync(firstId);
            await repository.DeleteAsync(testEntity);
            (await repository.CountAsync()).ShouldBe(0);
        }
    }
}
