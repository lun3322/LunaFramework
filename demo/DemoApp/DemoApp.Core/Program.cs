using System;
using Castle.Facilities.Logging;
using Castle.Services.Logging.NLogIntegration;
using Luna;

namespace DemoApp.Core
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var starter = Starter.Create<Runner>())
            {
                starter.Container.AddFacility<LoggingFacility>(m =>
                    m.LogUsing<NLogFactory>().WithConfig("NLog.config"));

                starter.Run();
            }
        }
    }
}
