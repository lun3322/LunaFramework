using System.Data;
using MySql.Data.MySqlClient;

namespace Luna.Repository.Dapper.Tests
{
    public class DapperDbConnectionProvider : IDbConnectionProvider
    {
        public IDbConnection GetDbConnection()
        {
            const string connstr = "server=10.3.4.11;user id=root;password=123qwe;database=test;";
            return new MySqlConnection(connstr);
        }
    }
}