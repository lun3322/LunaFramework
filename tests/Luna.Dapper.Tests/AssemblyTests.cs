using System.Linq;
using Castle.Core.Internal;
using Shouldly;
using Xunit;

namespace Luna.Dapper.Tests
{
    public class AssemblyTests
    {
        [Fact]
        public void AllReferencedAssembly_Test()
        {
            var rootAssembly = typeof(AssemblyTests).Assembly;
            var list = ReflectionUtil.GetApplicationAssemblies(rootAssembly).ToList();

            list.Count.ShouldBeGreaterThan(0);
        }
    }
}
