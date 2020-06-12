using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using Luna.Application.Dto;
using Luna.Dependency;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Luna.Web.Mvc.Filters
{
    /// <summary>
    /// 模型验证
    /// </summary>
    public class ModelVerificationFilter : IActionFilter, ISingletonDependency
    {
        public void OnActionExecuted(ActionExecutedContext context)
        {
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            if (context.ModelState.IsValid)
            {
                return;
            }

            var errorMessage = context.ModelState
                ?.FirstOrDefault(m => m.Value.ValidationState == ModelValidationState.Invalid).Value
                ?.Errors
                ?.FirstOrDefault()
                ?.ErrorMessage;

            context.HttpContext.Response.StatusCode = (int) HttpStatusCode.BadRequest;
            context.Result = new ObjectResult(ResponseVm.Failed(errorMessage, 400));
        }
    }
}
