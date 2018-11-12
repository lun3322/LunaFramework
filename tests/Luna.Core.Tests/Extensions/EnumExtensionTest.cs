using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using Luna.Extensions;
using Shouldly;
using Xunit;

namespace Luna.Core.Tests.Extensions
{
    public class EnumExtensionTest : TestBase
    {
        [Fact]
        public void GetDescription_Test()
        {
            var t1Des = TestEnum.T1.GetDescription();
            var t2Des = TestEnum.T2.GetDescription();

            t1Des.ShouldBe("测试1");
            t2Des.ShouldBe("测试2");
        }
    }

    public enum TestEnum
    {
        [Description("测试1")]
        T1,
        [Description("测试2")]
        T2
    }
}
