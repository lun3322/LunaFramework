using Castle.MicroKernel.Registration;
using Luna.Dependency;

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

        }

        public override void Setup()
        {
        }
    }
}