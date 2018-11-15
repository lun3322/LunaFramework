using Castle.Core.Logging;
using Luna.Application.Dto;
using Luna.Dependency;
using Luna.Web.Mvc.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Net;
using System.Reflection;
using System.Threading.Tasks;

namespace Luna.Web.Mvc.Filters
{
    public class LunaExceptionFilter : ExceptionFilterAttribute, ITransientDependency
    {
        private readonly ILogger _logger;

        public LunaExceptionFilter(ILogger logger)
        {
            _logger = logger;
        }

        public override void OnException(ExceptionContext context)
        {
            if (!context.ActionDescriptor.IsControllerAction())
            {
                return;
            }

            _logger.Error(context.Exception.ToString());
            // 判断返回值类型
            if (!IsObjectResult(context.ActionDescriptor.GetMethodInfo().ReturnType))
            {
                return;
            }

            context.Exception = null;
            context.HttpContext.Response.StatusCode = (int)HttpStatusCode.OK;
            context.Result = new ObjectResult(new ResponseVm("服务器内部错误", 503));
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
                if (typeof(JsonResult).GetTypeInfo().IsAssignableFrom(returnType) || typeof(ObjectResult).GetTypeInfo().IsAssignableFrom(returnType))
                {
                    return true;
                }

                return false;
            }

            return true;
        }
    }
}
