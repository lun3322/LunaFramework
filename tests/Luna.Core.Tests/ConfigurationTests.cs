using FluentAssertions;
using Microsoft.Extensions.Configuration;
using Xunit;

namespace Luna.Core.Tests
{
    public class ConfigurationTests
    {
        [Fact]
        public void GetSectionTest()
        {
            var builder = new ConfigurationBuilder();
            var configurationRoot = builder.Build();

            var section = configurationRoot.GetSection("oqwij:fqowej");
            section.Should().NotBeNull();
            section.Value.Should().BeNull();
        }
    }
}