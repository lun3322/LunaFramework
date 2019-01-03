using Luna.Dapper.Repository;
using Luna.Dependency;

namespace Luna.Dapper
{
    public class LunaDapperConfiguration : LunaConfiguration
    {
        public override void Initialize()
        {
            RepositoryRegister.Initialize(IocManager);
        }

        public override void Setup()
        {
        }
    }
}
