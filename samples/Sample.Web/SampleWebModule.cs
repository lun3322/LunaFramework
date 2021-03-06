﻿using Luna.Dependency;
using Luna.Web;
using Microsoft.Extensions.DependencyInjection;
using Sample.Core;

namespace Sample.Web
{
    [Dependency(
        typeof(LunaWebModule),
        typeof(SampleCoreModule)
    )]
    public class SampleWebModule : LunaModule
    {
        public override void ConfigureServices(IServiceCollection services)
        {
        }
    }
}