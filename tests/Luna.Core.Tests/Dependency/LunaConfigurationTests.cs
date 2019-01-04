using System;
using System.Collections.Generic;
using System.Text;
using Luna.Dependency;
using Shouldly;
using Xunit;
using Xunit.Abstractions;

namespace Luna.Core.Tests.Dependency
{
    public class LunaConfigurationTests : TestBase
    {
        private readonly ITestOutputHelper _output;

        public LunaConfigurationTests(ITestOutputHelper output)
        {
            _output = output;
        }

        [Fact]
        public void LunaConfiguration_Is_In_Ioc_Test()
        {
            var configurations = IocManager.ResolveAll<ILunaConfiguration>();
            var length = configurations.Length;
            _output.WriteLine(length.ToString());

            length.ShouldBeGreaterThan(0);
        }
    }
}
