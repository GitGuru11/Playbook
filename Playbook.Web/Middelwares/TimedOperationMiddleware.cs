namespace Playbook.Instrumentation.Web
{
    using System;
    using System.Diagnostics;
    using System.Globalization;
    using System.Net;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Http.Extensions;
    using Playbook.Common.Instrumentation;


    /// <summary>
    /// ASP.NET Core middleware which is used to measure the performance of REST APIs.
    /// </summary>
    public class TimedOperationMiddleware
    {
        internal const string InstrumentationLoggingContextKey = "InstrumentationLoggingContextKey";

        private const HttpStatusCode OperationCanceledStatusCode = (HttpStatusCode)418;


        /// <summary>
        /// The next ASP.NET Core middleware in line to be invoked.
        /// </summary>
        private RequestDelegate nextMiddleware;

        /// <summary>
        /// Initializes a new instance of the <see cref="TimedOperationMiddleware"/> class.
        /// </summary>
        public TimedOperationMiddleware(RequestDelegate nextMiddleware)
        {
            this.nextMiddleware = nextMiddleware;
        }

        /// <summary>
        /// Measure the performance of API.
        /// </summary>
        public async Task Invoke(HttpContext context)
        {
            var watch = new Stopwatch();
            var message = string.Empty;
            var code = HttpStatusCode.InternalServerError;
            var args = new InstrumentationArgs();

            try
            {
                watch.Start();
                await this.nextMiddleware(context);
            }
            catch (OperationCanceledException exception)
            {
                code = OperationCanceledStatusCode;
                args.Add("exception", exception);
                message = exception.Message;
            }
            catch (Exception exception)
            {
                args.Add("exception", exception);

                message = exception.Message;
                if (exception.InnerException != null)
                {
                    code = HttpStatusCode.InternalServerError;
                    message = message + Environment.NewLine + exception.InnerException;
                }
            }
            finally
            {
                watch.Stop();
                InstrumentationContext instrumentationContext = null;
                var duration = watch.ElapsedMilliseconds.ToString(CultureInfo.InvariantCulture);
                var request = context.Request;
                var response = context.Response;

                object tenant;
                context.Items.TryGetValue("tenant", out tenant);
                if (tenant != null)
                {
                    args.Add("tenant", tenant as string);
                }

                object iContext;
                context.Items.TryGetValue(InstrumentationLoggingContextKey, out iContext);
                if (iContext != null)
                {
                    instrumentationContext = (iContext as InstrumentationContext).AsCurrent();
                }
                else
                {
                    if (!string.IsNullOrEmpty(message))
                    {
                        instrumentationContext = new InstrumentationContext();
                        instrumentationContext.AsCurrent();
                        instrumentationContext.Error("PLAYBOOK.Service.Framework.Exception", "******* Attention Framework Exception!! ******* " + message);
                    }
                }

                if (instrumentationContext != null)
                {
                    args.Add("requestMethod", request.Method);
                    args.Add("requestUri", request.GetEncodedUrl());
                    args.Add("requestContentType", request.ContentType);
                    args.Add("requestContentLength", request.ContentLength);

                    code = (HttpStatusCode)response.StatusCode;
                    args.Add("statusCode", (int)code);

                    args.Add("responseTime", string.Format("{0} milliseconds", duration));
                    args.Add("message", message);


                    var codeString = (code == OperationCanceledStatusCode) ? "Canceled" : code.ToString();

                    string format = codeString;
                    if (instrumentationContext != null && !string.IsNullOrEmpty(instrumentationContext.ApiName))
                    {
                        format += !string.IsNullOrEmpty(message) ? string.Format(" Message:\"{0}\"", message) : string.Empty;
                    }

                    instrumentationContext.Log(
                        instrumentationContext.ApiName + ".Status." + codeString, InstrumentationLevel.Information,
                        format,
                        args);
                }
            }
        }
    }
}
