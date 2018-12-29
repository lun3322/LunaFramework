using Dapper;
using Luna.Repository;
using Shouldly;
using System;
using Xunit;

namespace Luna.Dapper.Tests.Repository
{
    public class DapperRepositoryTests : TestBase
    {
        private readonly IRepository<TestEntity> _repository;
        private readonly IDbConnectionProvider _dbConnectionProvider;
        public DapperRepositoryTests()
        {
            _repository = IocManager.Resolve<IRepository<TestEntity>>();
            _dbConnectionProvider = IocManager.Resolve<IDbConnectionProvider>();

            CreateTable();
        }

        private void CreateTable()
        {
            const string strsql = @"
CREATE TABLE 'TestEntity' (
  'Id' INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
  'Name' text NOT NULL,
  'Age' INTEGER NOT NULL
);;
";
            _dbConnectionProvider.GetDbConnection().Execute(strsql);
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

            firstInsert.ShouldBe(1);
            secondInsert.ShouldBe(2);

            _repository.Count().ShouldBe(2);
        }
    }
}
