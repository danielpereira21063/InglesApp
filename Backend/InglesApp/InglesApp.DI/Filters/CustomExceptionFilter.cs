using InglesApp.Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace InglesApp.Di.Filters
{
    public class CustomExceptionFilter : ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            //bool isAjaxCall = context.HttpContext.Request.Headers["x-requested-with"] == "XMLHttpRequest";

            //if (isAjaxCall)
            //{
            //}

            context.HttpContext.Response.ContentType = "application/json";
            context.HttpContext.Response.StatusCode = context.Exception is DomainException ? StatusCodes.Status502BadGateway : StatusCodes.Status500InternalServerError;
            context.Result = context.Exception is DomainException dominio ? new JsonResult(dominio.MsgErros) : new JsonResult("Ocorreu um erro desconhecido");
            context.ExceptionHandled = true;


            base.OnException(context);
        }
    }
}
