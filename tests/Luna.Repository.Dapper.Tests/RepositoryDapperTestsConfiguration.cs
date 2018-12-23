using DapperExtensions.Sql;
using Luna.Dependency;

namespace Luna.Repository.Dapper.Tests
{
    public class RepositoryDapperTestsConfiguration : LunaConfiguration
    {
        public override void Initialize()
        {
            Logger.Info("测试");
        }

        public override void Setup()
        {
            DapperExtensions.DapperExtensions.SqlDialect = new MySqlDialect();
        }
    }
}