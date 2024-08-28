using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Playbook.Service.Contracts;
using Playbook.Web;
using Playbook.Web.Extension;


// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Playbook.Service.Controllers
{
    [ApiController]
    public class BaseController : Controller
    {
        /// <summary>
        /// Helper method to error response.
        /// <paramref name="statusCode"/> and an <see cref="ErrorResponse"/> object in the body.
        /// </summary>
        /// <param name="statusCode">HTTP status code to use when invalid.</param>
        /// <param name="errorCode">Healthlog error code to use.</param>
        /// <param name="messageParameters">Optional parameter to format into the error message.</param>        
        protected async internal Task<IActionResult> CreateErrorResponseAsync(HttpStatusCode statusCode, ErrorCode errorCode, params object[]? args)
        {
            var error = ErrorResponse.Create(errorCode, args);
            this.HttpContext.Items.Add("ErrorCode", (int)error.ErrorCode);
            this.HttpContext.Items.Add("ErrorMessage", error.ErrorMessage);
            return await this.Request.CreateResponseAsync(statusCode, error);
        }

        /// <summary>
        /// Helper method to error response.
        /// <paramref name="statusCode"/> and an <see cref="ErrorResponse"/> object in the body.
        /// </summary>
        /// <param name="statusCode">HTTP status code to use when invalid.</param>
        /// <param name="errorCode">Healthlog error code to use.</param>
        /// <param name="messageParameters">Optional parameter to format into the error message.</param>        
        protected async internal Task<IActionResult> CreateMissingParameterErrorResponseAsync(HttpStatusCode statusCode, string paramName)
        {
            var error = ErrorResponse.Create(ErrorCode.MissingOrInvalidRequestParameter, paramName);
            this.HttpContext.Items.Add("ErrorCode", (int)error.ErrorCode);
            this.HttpContext.Items.Add("ErrorMessage", error.ErrorMessage);
            return await this.Request.CreateResponseAsync(statusCode, error);
        }
    }


}

