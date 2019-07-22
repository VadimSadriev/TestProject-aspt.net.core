using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using TestProject.Core;

namespace TestProject.WebServer
{
    public class ExceptionFilter : Attribute, IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            if (!context.ExceptionHandled)
            {
                context.Result = new JsonResult(new ApiResponse
                {
                    ErrorMessage = "Произошла ошибка при обработке вашего запроса"
                });
            }

            context.ExceptionHandled = true;
        }
    }
}
