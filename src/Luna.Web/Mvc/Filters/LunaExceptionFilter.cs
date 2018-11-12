using System;
using System.Collections.Generic;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Castle.Core.Logging;
using Luna.Application.Dto;
using Luna.Dependency;
using Luna.Web.Mvc.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Luna.Web.Mvc.Filters
{
    public class LunaExceptionFilter : ExceptionFilterAttribute, ITransientDependency
    {
        public ILogger Logger { get; set; }

        public LunaExceptionFilter()
        {
            Logger = NullLogger.Instance;
        }

        public override void OnException(ExceptionContext context)
        {
            if (!context.ActionDescriptor.IsControllerAction())
            {
                return;
            }

            Logger.Error(context.Exception.ToString());
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
