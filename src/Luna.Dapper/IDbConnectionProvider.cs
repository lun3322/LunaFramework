using Luna.Dependency;
using System.Data;

namespace Luna.Dapper
{
    public interface IDbConnectionProvider : ISingletonDependency
    {
        IDbConnection GetDbConnection();
    }
}
