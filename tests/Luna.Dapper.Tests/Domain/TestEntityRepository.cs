using Dapper.FastCrud;
using Luna.Dapper.Repository;
using Luna.Dapper.Tests.Repository;
using Luna.Repository;
using MySql.Data.MySqlClient;

namespace Luna.Dapper.Tests.Domain
{
    public class TestEntityRepository : DapperRepositoryBase<TestEntity, int>, ITestEntityRepository
    {
        public TestEntityRepository() : base(new MySqlConnection(AppSetting.MysqlConnectionString))
        {
        }
    }
}
