using System.Data;
using Luna.Dependency;

namespace Luna.Repository
{
    public interface ILunaDbContext : ITransientDependency
    {
        IDbConnection DbConnection { get; set; }
    }
}
