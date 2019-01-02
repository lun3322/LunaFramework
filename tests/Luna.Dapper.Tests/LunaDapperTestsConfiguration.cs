using Castle.MicroKernel.Registration;
using Dapper.FastCrud;
using Luna.Dependency;
using System.Data;
using System.Data.SQLite;

namespace Luna.Dapper.Tests
{
    public class LunaDapperTestsConfiguration : LunaConfiguration
    {
        private readonly IIocManager _iocManager;

        public LunaDapperTestsConfiguration(IIocManager iocManager)
        {
            _iocManager = iocManager;
        }

        public override void Initialize()
        {

        }

        public override void Setup()
        {
            OrmConfiguration.DefaultDialect = SqlDialect.SqLite;
        }
    }
}
