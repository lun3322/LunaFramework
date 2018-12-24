using System;
using Castle.Windsor.MsDependencyInjection;
using Luna.Dependency;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Luna.Core.Tests
{
    public class TestBase
    {
        public LunaStarter LunaStarter { get; private set; }
        public IIocManager IocManager { get; set; }

        public TestBase()
        {
            var builder = new ConfigurationBuilder()
                .AddInMemoryCollection()
                .SetBasePath(Environment.CurrentDirectory)
                .AddJsonFile("appsettings.json", optional: true);
            var configuration = builder.Build();

            var collection = new ServiceCollection();

            LunaStarter = LunaStarter.Create<TestBase>();
            LunaStarter.IocManager.IocContainer.AddServices(collection);
            LunaStarter.Initialize();
            IocManager = LunaStarter.IocManager;
        }
    }
}