using Luna.Dependency;
using System.Data;
using System.Threading;

namespace Luna.Dapper
{
    public class DbConnectionProvider : IDbConnectionProvider
    {
        private static readonly AsyncLocal<IDbConnection> AsyncLocal = new AsyncLocal<IDbConnection>();
        private readonly IIocManager _iocManager;

        public DbConnectionProvider(IIocManager iocManager)
        {
            _iocManager = iocManager;
        }

        public IDbConnection GetDbConnection()
        {
            if (AsyncLocal.Value == null)
            {
                lock (AsyncLocal)
                {
                    if (AsyncLocal.Value == null)
                    {
                        AsyncLocal.Value = _iocManager.Resolve<IDbConnection>();
                        AsyncLocal.Value.Open();
                    }
                }
            }

            return AsyncLocal.Value;
        }
    }
}
