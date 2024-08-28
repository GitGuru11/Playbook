namespace Playbook.Service.Contracts
{
    using System;
    using Newtonsoft.Json;

    public class ServiceStatus
    {
        /// <summary>
        /// Gets or sets the value for the instanceId.
        /// </summary>

        public virtual string? Id { get; set; }

        /// <summary>
        /// Gets or sets the value for the instanceId.
        /// </summary>

        public string? Status { get; set; }

        /// <summary>
        /// Gets or sets the value for the instanceId.
        /// </summary>

        public string? InstanceId { get; set; }

        /// <summary>
        /// Gets or sets the parameter upTime.
        /// </summary>

        public string? UpTime { get; set; }

        /// <summary>
        /// Gets or sets the value for the callCount.
        /// </summary>

        public long CallCount { get; set; }

        /// <summary>
        /// Gets or sets the value for the version.
        /// </summary>

        public string? ComponentVersion { get; set; }

        /// <summary>
        /// Gets or sets the value for the version.
        /// </summary>

        public string? ComponentName { get; set; }
    }
}
