using Luna.Web.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;

namespace Luna.Web.Mvc.Extensions
{
    internal static class LunaMvcOptionsExtension
    {
        public static void Configure(this MvcOptions @this)
        {
            @this.Filters.Add<LunaExceptionFilter>();
            @this.Filters.Add<ModelVerificationFilter>();
        }
    }
}