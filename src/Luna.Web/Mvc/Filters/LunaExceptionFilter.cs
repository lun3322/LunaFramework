using System;
using System.Net;
using System.Reflection;
using System.Threading.Tasks;
using Luna.Application.Dto;
using Luna.Dependency;
using Luna.Exceptions;
using Luna.Web.Mvc.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;

namespace Luna.Web.Mvc.Filters
{
    public class LunaExceptionFilter : ExceptionFilterAttribute, IScopedDependency
    {
        private readonly ILogger<LunaExceptionFilter> _logger;

        public LunaExceptionFilter(ILogger<LunaExceptionFilter> logger)
        {
            _logger = logger;
        }

        public override void OnException(ExceptionContext context)
        {
            _logger.LogError(context.Exception.ToString());
            if (!context.ActionDescriptor.IsControllerAction()) return;

            // 判断返回值类型
            if (!IsObjectResult(context.ActionDescriptor.GetMethodInfo().ReturnType)) return;

            if (context.Exception is LunaUserFriendlyException ex)
            {
                context.HttpContext.Response.StatusCode = (int) HttpStatusCode.OK;
                context.Result = new ObjectResult(new ResponseVm(ex.Message, ex.Code));
                return;
            }

            context.Exception = null;
            context.HttpContext.Response.StatusCode = (int) HttpStatusCode.InternalServerError;
            context.Result = new ObjectResult(new ResponseVm("服务器内部错误", 500));
        }

        public static bool IsObjectResult(Type returnType)
        {
            if (returnType == typeof(Task))
            {
                returnType = typeof(void);
            }
            else if (returnType.GetTypeInfo().IsGenericType && returnType.GetGenericTypeDefinition() == typeof(Task<>))
            {
                returnType = returnType.GenericTypeArguments[0];
            }

            if (typeof(IActionResult).GetTypeInfo().IsAssignableFrom(returnType))
            {
                if (typeof(JsonResult).GetTypeInfo().IsAssignableFrom(returnType) ||
                    typeof(ObjectResult).GetTypeInfo().IsAssignableFrom(returnType))
                {
                    return true;
                }

                return false;
            }

            return true;
        }
    }
}