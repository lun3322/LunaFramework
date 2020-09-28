using System.Data;
using System.Data.SqlClient;
using Luna.Dependency;
using Microsoft.Extensions.Configuration;

namespace Luna.Dapper
{
    public class LunaDbContextManager : ILunaDbContextManager, IScopedDependency
    {
        private readonly IConfiguration _configuration;

        public LunaDbContextManager(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IDbConnection GetConnection(string name = "Default")
        {
            var conn = new SqlConnection(GetConnectionString(name));
            conn.Open();
            return conn;
        }

        public string GetConnectionString(string name)
        {
            var connectionString = _configuration.GetConnectionString(name);
            return connectionString;
        }
    }
}