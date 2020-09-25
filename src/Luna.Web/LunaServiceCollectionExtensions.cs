using System;
using Luna.Dependency;
using Luna.Web.Mvc.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace Luna.Web
{
    public static class LunaServiceCollectionExtensions
    {
        public static IServiceCollection AddLuna<TModule>(this IServiceCollection services,
            Action<LunaStarterOption> action = null) where TModule : LunaModule
        {
            var opt = new LunaStarterOption();
            action?.Invoke(opt);

            if (opt.EnableLunaFilters)
            {
                services.Configure<MvcOptions>(mvcOpt => { mvcOpt.Configure(); });
                // 为了启用全局模型验证
                services.Configure<ApiBehaviorOptions>(o => { o.SuppressModelStateInvalidFilter = true; });
            }

            var start = LunaStarter.Create<TModule>(services, action);
            services.AddSingleton(start);

            return services;
        }
    }
}