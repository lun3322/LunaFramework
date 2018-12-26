using Luna.Dependency;
using Shouldly;
using Xunit;

namespace Luna.Core.Tests.Dependency
{
    public class LunaConfigurationTests : TestBase
    {
        [Fact]
        public void Multi_LunaConfiguration_Test()
        {
            var resolveAll = IocManager.ResolveAll<ILunaConfiguration>();

            resolveAll.Length.ShouldBeGreaterThan(1);

            resolveAll.ShouldContain(m => m.GetType() == typeof(LunaConfigurationTest1));
            resolveAll.ShouldContain(m => m.GetType() == typeof(LunaConfigurationTest2));
        }
    }
}
