using System;
using Castle.Windsor.MsDependencyInjection;
using Luna.Dependency;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Luna.Core.Tests
{
    public class TestBase: LunaTestBase<TestBase>
    {
        public TestBase()
        {
            var builder = new ConfigurationBuilder()
                .AddInMemoryCollection()
                .SetBasePath(Environment.CurrentDirectory)
                .AddJsonFile("appsettings.json", optional: true);
            var configuration = builder.Build();

            var collection = new ServiceCollection();
            LunaStarter.IocManager.IocContainer.AddServices(collection);
        }
    }
}