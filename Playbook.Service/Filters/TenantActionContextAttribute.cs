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
    using Playbook.Common.Tenant;

    public sealed class TenantActionContextAttribute : ActionFilterAttribute
    {
        /// <summary>
        /// The tenant context key
        /// </summary>
        internal const string TenantContextKey = "TenantContextKey";

        /// <summary>
        /// The tenant context identifier header name
        /// </summary>
        public const string TenantNameHeaderKey = "x-tenant";

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            try
            {
                var tenantContext = AsTenantContext(context);
                if (tenantContext != null)
                {
                    tenantContext.AsCurrent();

                    // In case of exception, call context will be lost, so the instrumentation context should be stored in current httpcontext
                    context.HttpContext.Items[TenantContextKey] = tenantContext;

                    context.HttpContext.Response.Headers.Add(TenantNameHeaderKey, tenantContext.TenantName);
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(string.Format(CultureInfo.CurrentCulture, "[{0}] failed to set tenant context for request '{1}'\r\n{2}", DateTime.UtcNow, UriHelper.GetDisplayUrl(context.HttpContext.Request), exception));
            }
        }


        /// <summary>
        /// Creates an tenant context from a ActionExecutingContext.
        /// </summary>
        public static TenantContext? AsTenantContext(ActionExecutingContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            string? tenantName = null;

            tenantName = context.HttpContext.Request.Headers[TenantNameHeaderKey].FirstOrDefault();

            TenantContext? tenantContext = null;
            if (tenantName != null)
            {
                tenantContext = new TenantContext(tenantName)
                {
                    TenantName = tenantName
                };
            }
            return tenantContext;
        }
    }
}
