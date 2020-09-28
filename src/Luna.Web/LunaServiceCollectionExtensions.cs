using System;
using System.Text.Encodings.Web;
using System.Text.Unicode;
using Luna.Dependency;
using Luna.Web.Mvc.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.WebEncoders;

namespace Luna.Web
{
    public static class LunaServiceCollectionExtensions
    {
        public static IServiceCollection AddLuna<TModule>(this IServiceCollection services,
            Action<LunaStarterOption> action = null) where TModule : LunaModule
        {
            var opt = new LunaStarterOption();
            action?.Invoke(opt);

            services.Configure<MvcOptions>(mvcOpt => { mvcOpt.Configure(services, opt); });

            // 解决中文被编码的问题
            services.Configure<WebEncoderOptions>(options => { options.TextEncoderSettings = new TextEncoderSettings(UnicodeRanges.All); });

            LunaStarter.StartUp<TModule>(services, action);
            return services;
        }
    }
}