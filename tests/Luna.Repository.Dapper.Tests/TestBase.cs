using System;
using Castle.Core.Logging;
using Castle.Facilities.Logging;
using Castle.Windsor.MsDependencyInjection;
using Luna.Dependency;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Luna.Repository.Dapper.Tests
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
//            collection.AddOptions()
//                .Configure<AppSetting>(configuration.GetSection("AppSetting"));

            LunaStarter = LunaStarter.Create<TestBase>();
            LunaStarter.IocManager.IocContainer.AddServices(collection);
            LunaStarter.IocManager.IocContainer.AddFacility<LoggingFacility>(m => m.LogUsing<NullLogFactory>());
            LunaStarter.Initialize();
            IocManager = LunaStarter.IocManager;
        }
    }
}