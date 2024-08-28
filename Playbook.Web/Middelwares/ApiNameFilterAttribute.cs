using System;

namespace Playbook.Instrumentation.Web
{
    /// <summary>
    /// Filter for monitoring performance. Times action and periodically outputs summary
    /// to instrumentation log output.
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, Inherited = false, AllowMultiple = false)]
    public sealed class ApiNameFilterAttribute : Attribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ApiNameFilterAttribute"/> class.
        /// </summary>
        /// <param name="controllerAction">
        /// The controller action that was called.
        /// </param>
        public ApiNameFilterAttribute(string apiName)
        {
            if (string.IsNullOrWhiteSpace(apiName))
            {
                throw new ArgumentNullException("apiName");
            }

            this.ApiName = apiName;
        }

        /// <summary>
        /// Gets the controller action.
        /// </summary>
        public string ApiName { get; private set; }
    }
}
