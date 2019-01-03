using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using Luna.Repository;

namespace Luna.Dapper.Repository
{
    public abstract class LunaDapperDbContext : LunaDbContext
    {
        protected LunaDapperDbContext(IDbConnection dbConnection)
            : base(dbConnection)
        {
        }
    }
}
