using Dapper;
using Luna.Repository;
using Shouldly;
using System;
using Luna.Dapper.Tests.Entities;
using Xunit;
using Xunit.Abstractions;

namespace Luna.Dapper.Tests.Repository
{
    public class DapperRepositoryTests : TestBase
    {
        private readonly ITestOutputHelper _output;
        private readonly IRepository<TestEntity> _repository;
        public DapperRepositoryTests(ITestOutputHelper output)
        {
            _output = output;
            _repository = IocManager.Resolve<IRepository<TestEntity>>();
        }

        [Fact]
        public void Insert_Test()
        {
            var entity = new TestEntity
            {
                Name = Guid.NewGuid().ToString(),
                Age = 33
            };
            var firstInsert = _repository.Insert(entity);
            var secondInsert = _repository.Insert(entity);

            _output.WriteLine(firstInsert.ToString());
            _output.WriteLine(secondInsert.ToString());

            firstInsert.ShouldBeGreaterThan(0);
            secondInsert.ShouldBeGreaterThan(0);

            _repository.Count().ShouldBe(2);
            _repository.Delete(firstInsert);
            _repository.Delete(secondInsert);
            _repository.Count().ShouldBe(0);
        }
    }
}
