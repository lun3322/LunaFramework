using Luna.Web.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace Luna.Web.Mvc.Extensions
{
    internal static class LunaMvcOptionsExtension
    {
        public static void Configure(this MvcOptions @this, IServiceCollection services, LunaStarterOption option)
        {
            if (option.EnableLunaGlobalExceptionHandle)
            {
                // 全局异常处理
                @this.Filters.Add<LunaExceptionFilter>();
            }

            if (option.EnableLunaModelValid)
            {
                // 为了启用全局模型验证
                services.Configure<ApiBehaviorOptions>(o => { o.SuppressModelStateInvalidFilter = true; });
                @this.Filters.Add<ModelVerificationFilter>();
            }
        }
    }
}