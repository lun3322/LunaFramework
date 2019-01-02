using Luna.Repository;
using System.Data.SQLite;

namespace Luna.Dapper.Tests.Repository
{
    public class LunaDapperTestDbContext : LunaDbContext
    {
        public TestEntity TestEntity { get; set; }
        public TestEntityTwo TestEntityTwo { get; set; }

        public LunaDapperTestDbContext()
            : base(new SQLiteConnection("Data Source=:memory:;Version=3;New=True;"))
        {
        }
    }
}
