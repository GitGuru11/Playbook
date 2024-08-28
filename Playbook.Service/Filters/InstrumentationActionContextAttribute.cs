namespace Playbook.Service.Filters
{
    using System;
    using System.Diagnostics;
    using System.Globalization;
    using Microsoft.AspNetCore.Http.Extensions;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Controllers;
    using Microsoft.AspNetCore.Mvc.Filters;
    using Playbook.Common.Instrumentation;
    using Playbook.Instrumentation.Web;

    public sealed class InstrumentationActionContextAttribute : ActionFilterAttribute
    {
        /// <summary>
        /// The instrumentation context key
        /// </summary>
        internal const string InstrumentationLoggingContextKey = "InstrumentationLoggingContextKey";

        /// <summary>
        /// The tenant context identifier header name
        /// </summary>
        public const string TenantNameHeaderKey = "x-tenant";

        /// <summary>
        /// The instrumentation context identifier header name
        /// </summary>
        public const string TraceIdHeaderKey = "x-aviso-traceid";

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            try
            {
                var instrumentationContext = AsInstrumentationContext(context);
                if (instrumentationContext != null)
                {
                    instrumentationContext.AsCurrent();

                    // In case of exception, call context will be lost, so the instrumentation context should be stored in current httpcontext
                    context.HttpContext.Items[InstrumentationLoggingContextKey] = instrumentationContext;

                    context.HttpContext.Response.Headers.Add(TraceIdHeaderKey, instrumentationContext.TraceId);
                }
            }
            catch (Exception exception)
            {
                Trace.WriteLine(string.Format(CultureInfo.CurrentCulture, "[{0}] failed to set instrumentation context for request '{1}'\r\n{2}", DateTime.UtcNow, UriHelper.GetDisplayUrl(context.HttpContext.Request), exception));
            }
        }


        /// <summary>
        /// Creates an instrumentation context from a ActionExecutingContext.
        /// </summary>
        public static InstrumentationContext AsInstrumentationContext(ActionExecutingContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }


            string? serverOrigin = null;
            string? traceId = null;
            string? tenant = null;
            string? controllerName = null;
            string? actionName = null;
            string? fullName = null;
            string? apiName = null;

            var request = context.HttpContext.Request;

            serverOrigin = request.GetEncodedUrl();
            traceId = request.Headers[TraceIdHeaderKey].FirstOrDefault();
            tenant = request.Headers[TenantNameHeaderKey].FirstOrDefault();

            var controllerActionDescriptor = context.ActionDescriptor as ControllerActionDescriptor;
            if (controllerActionDescriptor != null)
            {
                controllerName = controllerActionDescriptor.ControllerName;
                actionName = controllerActionDescriptor.ActionName;
                fullName = controllerActionDescriptor.DisplayName;
                var attribute = (ApiNameFilterAttribute)controllerActionDescriptor.MethodInfo.GetCustomAttributes(typeof(ApiNameFilterAttribute), false).FirstOrDefault();
                if (attribute != null)
                {
                    apiName = attribute.ApiName;
                }
            }

            InstrumentationContext instrumentationContext = null;
            if (serverOrigin != null || traceId != null || tenant != null || apiName != null)
            {
                instrumentationContext = new InstrumentationContext(traceId, serverOrigin)
                {
                    ApiName = apiName,
                    Tenant = tenant,
                    InstanceNumber = HostConfiguration.ApplicationId,
                    RoleName = HostConfiguration.ServiceName,
                    Environment = HostConfiguration.Environment,
                    Operator = HostConfiguration.Operator
                };
            }
            return instrumentationContext;
        }
    }
}
