using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Luna.Web.Mvc.Middleware
{
    public class LunaExceptionLogMiddleware
    {
        private readonly RequestDelegate _next;

        public LunaExceptionLogMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context, ILogger<LunaExceptionLogMiddleware> logger)
        {
            try
            {
                await _next.Invoke(context);
            }
            catch (Exception e)
            {
                logger.LogError(e.ToString());
                throw;
            }
        }
    }
}