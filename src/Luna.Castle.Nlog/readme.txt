使用方法:

using (var starter = LunaStarter.Create<Program>())
{
    starter.IocManager.IocContainer.AddFacility<LoggingFacility>(m =>
        m.LogUsing<NLogFactory>().WithConfig("NLog.config"));

    starter.Initialize();
}