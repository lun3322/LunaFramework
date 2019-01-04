using Luna.Dapper.Repository;
using Luna.Dapper.Tests.Repository;
using MySql.Data.MySqlClient;

namespace Luna.Dapper.Tests.Domain
{
    public class TestEntityTwoRepository : DapperRepositoryBase<TestEntityTwo, int>, ITestEntityTwoRepository
    {
        public TestEntityTwoRepository() : base(new MySqlConnection(AppSetting.MysqlConnectionString))
        {
        }
    }
}
