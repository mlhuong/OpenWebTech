using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using OpenWebTech.Contacts.Services.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace OpenWebTech.Contacts.Web.Controllers
{
    [ApiExplorerSettings(IgnoreApi = true)]
    public class ErrorsController : ControllerBase
    {
        [Route("error")]
        public ErrorResponse Error()
        {
            var context = HttpContext.Features.Get<IExceptionHandlerFeature>();
            var exception = context?.Error; // Your exception
            var code = 500; // Internal Server Error by default

            if (exception is NotFoundException) code = (int)HttpStatusCode.NotFound; // Not Found
            else if (exception is DuplicateException) code = (int)HttpStatusCode.BadRequest; // Unauthorized

            Response.StatusCode = code; // You can use HttpStatusCode enum instead

            return new ErrorResponse()
            {
                Message = exception.Message
            };
        }
    }
}
