using Castle.Windsor.MsDependencyInjection;
using Luna.Dependency;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Luna.Dapper.Tests
{
    public class TestBase : LunaTestBase<TestBase>
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
