using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Castle.Core.Logging;
using Microsoft.AspNetCore.Http;

namespace Luna.Web.Mvc.Middleware
{
    public class LunaExceptionLogMiddleware
    {
        private readonly RequestDelegate _next;

        public LunaExceptionLogMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context, ILogger logger)
        {
            try
            {
                await _next.Invoke(context);
            }
            catch (Exception e)
            {
                logger.Error(e.ToString());
                throw;
            }
        }
    }
}