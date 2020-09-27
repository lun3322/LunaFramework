using Castle.Core.Configuration;
using FluentAssertions;
using Moq;
using Xunit;

namespace Luna.Dapper.Tests
{
    public class DbContextManagerTests
    {
        [Fact]
        public void GetConnectionStringIsAJoke()
        {
            const string name = "name";
            const string returnValue = "returnValue";

            var mock = new Mock<ILunaDbContextManager>();
            mock.Setup(m => m.GetConnectionString(name))
                .Returns(returnValue)
                .Verifiable();

            var connectionString = mock.Object.GetConnectionString(name);
            connectionString.Should().Be(returnValue);

            mock.Verify();
        }
    }
}