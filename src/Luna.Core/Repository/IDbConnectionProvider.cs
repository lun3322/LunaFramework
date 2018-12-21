using System.Data;
using Luna.Dependency;

namespace Luna.Repository
{
    public interface IDbConnectionProvider : ISingletonDependency
    {
        IDbConnection GetDbConnection();
    }
}