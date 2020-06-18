using System;
using Castle.Windsor.MsDependencyInjection;
using Luna.Web.Mvc.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Luna.Web
{
    public static class LunaServiceCollectionExtensions
    {
        public static IServiceProvider AddLuna<TModule>(this IServiceCollection services
            , Action<LunaStarterOption> action = null) where TModule : class
        {
            services.Replace(ServiceDescriptor.Transient<IControllerActivator, ServiceBasedControllerActivator>());

            var opt = new LunaStarterOption();
            action?.Invoke(opt);
            if (opt.EnableLunaFilters)
            {
                services.Configure<MvcOptions>(mvcOpt => { mvcOpt.Configure(); });
                // 为了启用全局模型验证
                services.Configure<ApiBehaviorOptions>(o => { o.SuppressModelStateInvalidFilter = true; });
            }

            var start = LunaStarter.Create<TModule>(action);
            services.AddSingleton(start);

            return WindsorRegistrationHelper.CreateServiceProvider(start.IocManager.IocContainer, services);
        }
    }
}