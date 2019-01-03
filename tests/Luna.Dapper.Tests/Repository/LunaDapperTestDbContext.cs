using Luna.Repository;
using Dapper;
using Luna.Dapper.Repository;
using Luna.Dapper.Tests.Entities;
using MySql.Data.MySqlClient;

namespace Luna.Dapper.Tests.Repository
{
    public class LunaDapperTestDbContext : LunaDapperDbContext
    {
        public TestEntity TestEntity { get; set; }
        public TestEntityTwo TestEntityTwo { get; set; }
        public TestEntityThree TestEntityThree { get;set; }

        public LunaDapperTestDbContext()
            : base(new MySqlConnection("Server=10.3.4.11;Database=tesdb;Uid=root;Pwd=abcd123-123-123;"))
        {

        }
    }
}
