using Castle.Facilities.Logging;
using Castle.Services.Logging.NLogIntegration;
using Luna;

namespace DemoApp.Core
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var starter = LunaStarter.Create<Program>())
            {
                starter.IocManager.IocContainer.AddFacility<LoggingFacility>(m =>
                    m.LogUsing<NLogFactory>().WithConfig("NLog.config"));

                starter.Initialize();

                var runner = starter.IocManager.Resolve<Runner>();
                runner.Run();
            }
        }
    }
}