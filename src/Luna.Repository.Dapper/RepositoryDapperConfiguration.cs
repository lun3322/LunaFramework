using Castle.MicroKernel.Registration;
using Luna.Dependency;
using Luna.Repository.Dapper;

namespace Luna.Repository
{
    public class RepositoryDapperConfiguration : LunaConfiguration
    {
        private readonly IIocManager _iocManager;

        public RepositoryDapperConfiguration(IIocManager iocManager)
        {
            _iocManager = iocManager;
        }

        public override void Initialize()
        {
            _iocManager.IocContainer.Register(
                Component.For(typeof(IRepository<>))
                    .ImplementedBy(typeof(DapperRepository<>))
                    .LifestyleTransient()
            );
            _iocManager.IocContainer.Register(
                Component.For(typeof(IRepository<,>))
                    .ImplementedBy(typeof(DapperRepository<,>))
                    .LifestyleTransient()
            );
        }

        public override void Setup()
        {
        }
    }
}