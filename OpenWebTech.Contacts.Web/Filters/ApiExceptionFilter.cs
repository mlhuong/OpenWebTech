using Microsoft.AspNetCore.Mvc.Filters;
using OpenWebTech.Contacts.Services.Exceptions;
using System.Net;

namespace OpenWebTech.Contacts.Web.Filters
{
    public class ApiExceptionFilter : ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            if (context.Exception is NotFoundException)
            {
                context.HttpContext.Response.StatusCode = (int)HttpStatusCode.NotFound;
                return;
            }
            else if (context.Exception is DuplicateException)
            {
                context.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            }
            else
            {
                context.HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            }

            base.OnException(context);
        }
    }
}
