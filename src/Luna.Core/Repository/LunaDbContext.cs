using System.Data;

namespace Luna.Repository
{
    public abstract  class LunaDbContext : ILunaDbContext
    {
        public IDbConnection DbConnection { get; set; }

        protected LunaDbContext(IDbConnection dbConnection)
        {
            DbConnection = dbConnection;
        }
    }
}
