using System;
using System.Collections.Generic;
using System.Text;
using Luna.Web.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace Luna.Web.Mvc.Extensions
{
    internal static class LunaMvcOptionsExtension
    {
        public static void Configure(this MvcOptions @this)
        {
            @this.Filters.Add(typeof(LunaExceptionFilter));
        }
    }
}
