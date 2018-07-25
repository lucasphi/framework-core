using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Text;

namespace Framework.Web.Mvc.Filters
{
    class FWExceptionFilterAttribute : ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            // Handles ajax erros as a JS alert.
            if (context.HttpContext.Request.IsAjaxRequest())
            {
                context.HttpContext.Response.StatusCode = 500;
                context.Result = new JsonResult(new
                {
                    message = context.Exception.Message
                });
            }
            
            base.OnException(context);
        }
    }
}
