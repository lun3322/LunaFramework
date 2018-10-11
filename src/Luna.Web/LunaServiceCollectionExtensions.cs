using System;
using Castle.Windsor.MsDependencyInjection;
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
            var start = LunaStarter.Create<TModule>();

            services.AddSingleton(start);

            return WindsorRegistrationHelper.CreateServiceProvider(start.IocManager.IocContainer, services);
        }
    }
}
