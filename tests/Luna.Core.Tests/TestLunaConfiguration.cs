using Castle.MicroKernel.Registration;
using Luna.Core.Tests.Repository;
using Luna.Dependency;
using Luna.Repository;

namespace Luna.Core.Tests
{
    public class TestLunaConfiguration : LunaConfiguration
    {
        private readonly IIocManager _iocManager;

        public TestLunaConfiguration(IIocManager iocManager)
        {
            _iocManager = iocManager;
        }

        public override void Initialize()
        {
            _iocManager.IocContainer.Register(
                Component.For(typeof(IRepository<>))
                    .ImplementedBy(typeof(TestRepository<>))
                    .LifestyleTransient()
            );
            _iocManager.IocContainer.Register(
                Component.For(typeof(IRepository<,>))
                    .ImplementedBy(typeof(TestRepository<,>))
                    .LifestyleTransient()
            );
        }

        public override void Setup()
        {
        }
    }
}