namespace Playbook.Service.Middlewares
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Net;

    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.Logging;
    using Microsoft.Net.Http.Headers;
    using Newtonsoft.Json;
    using Playbook.Common.Instrumentation;
    using Playbook.Service.Contracts;
    using Playbook.Web;

    public class ErrorHandlingMiddleware
    {
        internal const string InstrumentationLoggingContextKey = "InstrumentationLoggingContextKey";
        private readonly RequestDelegate next;
        private ILogger logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="ErrorHandlingMiddleware"/> class.
        /// </summary>
        /// <param name="next">Request delegate of next<see cref="ErrorHandlingMiddleware"/></param>
        /// <param name="loggerFactory"e.</param>
        public ErrorHandlingMiddleware(RequestDelegate next, ILoggerFactory loggerFactory)
        {
            this.next = next;
            this.logger = loggerFactory.CreateLogger<ErrorHandlingMiddleware>();

        }

        /// <summary>
        /// Inovoke the middelware.
        /// </summary>
        /// <param name="context">The http context.</param>        
        public async Task Invoke(HttpContext context /* other dependencies */)
        {
            //var args = new InstrumentationArgs();
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                object instrumentationContext;
                context.Items.TryGetValue(InstrumentationLoggingContextKey, out instrumentationContext);
                (instrumentationContext as InstrumentationContext).AsCurrent();
                InstrumentationContext.Current.Exception("Playbook.Service.UnHandledException", ex, ex.Source);
                await HandleExceptionAsync(context, ex);
            }
        }

        /// <summary>
        /// Handle exception asynchronously.
        /// </summary>
        /// <param name="context">The http context.</param>
        /// <param name="ex">The exception</param>        
        private static Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            var statuscode = HttpStatusCode.InternalServerError; // 500 if unexpected
            var errorResponse = ErrorResponse.Create(ErrorCode.Unspecified);
            if (ex is PlaybookErrorResponseException)
            {
                var playbookException = ex as PlaybookErrorResponseException;
                statuscode = playbookException.HttpStatusCode;
                errorResponse = playbookException.ErrorResponse;
            }
            return CreateErrorReponseAsync(context, statuscode, errorResponse);
        }

        /// <summary>
        /// Create error reponse asynchronously.
        /// </summary>
        /// <param name="context">The http context.</param>
        /// <param name="statusCode">The http status code.</param>
        /// <param name="errorResponse">The error response.</param>
        private static Task CreateErrorReponseAsync(HttpContext context, HttpStatusCode statusCode, ErrorResponse errorResponse)
        {
            var result = JsonConvert.SerializeObject(errorResponse);
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)statusCode;
            if (statusCode == HttpStatusCode.Unauthorized)
            {
                SetWWWAuthenticateResponseHeader(context);
            }
            return context.Response.WriteAsync(result);
        }

        /// <summary>
        /// Set the unuhorized response header.
        /// </summary>
        /// <param name="context">The http context.</param>
        private static void SetWWWAuthenticateResponseHeader(HttpContext context)
        {
            context.Response.Headers.Append(
                          HeaderNames.WWWAuthenticate,
                          string.Format("Totp realm={0} {1}", "aviso.com", "charset=utf-8")
                          );
        }

    }
}