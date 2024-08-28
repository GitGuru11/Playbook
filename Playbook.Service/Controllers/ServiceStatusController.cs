
namespace Playbooks.Service.Controllers
{

    using Playbook.Service.Contracts;
    using JsonApiSerializer;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Newtonsoft.Json;
    using Playbook.Bussiness;
    using Playbook.Common.Instrumentation;
    using Playbook.Service;
    using Swashbuckle.AspNetCore.Annotations;
    using System;
    using System.Diagnostics;
    using System.Threading;
    using Playbook.Service.Controllers;
    using Playbook.Instrumentation.Web;

    /// <summary>
    /// Controller for service status check.
    /// </summary>
    public class ServiceStatusController : BaseController
    {

        /// <summary>The ready response content.</summary>
        private const string Ready = "Ready";

        /// <summary>The service is not ready</summary>
        private const string NotReady = "Unavailable";

        /// <summary>Storage variable of the call count</summary>
        private static long callCount = 0L;

        /// <summary>Gets the call count (auto increamented)</summary>
        private static long CallCount => Interlocked.Increment(ref callCount);

        ///// <summary>Gets the instance id</summary>
        private static Lazy<string> InstanceId { get; } = new Lazy<string>(() => HostConfiguration.ApplicationId);

        /// <summary>Gets the version of code run</summary>
        private static Lazy<string> Version { get; } = new Lazy<string>(() => HostConfiguration.ServiceVersion);

        /// <summary>Gets the version of code run</summary>
        private static Lazy<string> Name { get; } = new Lazy<string>(() => HostConfiguration.ServiceName);

        /// <summary>Gets the current stop watch</summary>
        private static Stopwatch StartStopWatch { get; } = Stopwatch.StartNew();


        public ServiceStatusController()
        {
        }

        /// <summary>
        /// Gets a value indicating whether service is ready
        /// </summary>
        protected virtual bool IsReady()
        {
            return true;
        }

        /// <summary>Checks if service is fully initialized.</summary>
        /// <returns>Response of 200 if the service is initialized.</returns>   
        [HttpGet]
        [ApiNameFilter("Playbook.Api.ServiceStatus")]
        [Route(Routes.ServiceStatus)]
        [SwaggerOperation(Summary = "Service Status.", Tags = new[] { "Service Status" })]
        ////[Authorize]
        public IActionResult GetStatus()
        {
            ServiceStatus status;
            string? res = null;
            if (IsReady())
            {
                status = new ServiceStatus
                {
                    Id = CallCount.ToString(),
                    Status = Ready,
                    InstanceId = InstanceId.Value,
                    UpTime = StartStopWatch.Elapsed.ToString(),
                    CallCount = CallCount,
                    ComponentVersion = Version.Value,
                    ComponentName = Name.Value
                };
                //res = JsonConvert.SerializeObject(status, new JsonApiSerializerSettings());
                return this.StatusCode(StatusCodes.Status200OK, status);
            }
            else
            {
                status = new ServiceStatus
                {
                    Id = CallCount.ToString(),
                    Status = NotReady,
                    InstanceId = InstanceId.Value,
                    UpTime = StartStopWatch.Elapsed.ToString(),
                    CallCount = CallCount,
                    ComponentVersion = Version.Value,
                    ComponentName = Name.Value
                };
                //res = JsonConvert.SerializeObject(status, new JsonApiSerializerSettings());
                return this.StatusCode(StatusCodes.Status503ServiceUnavailable, status);
            }
        }
    }
}
