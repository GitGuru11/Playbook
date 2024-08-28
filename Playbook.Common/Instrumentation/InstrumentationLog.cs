namespace Playbook.Common.Instrumentation
{
    using System;
    using System.Collections.Generic;

    public class InstrumentationLog
    {
        public InstrumentationLog()
        {
            this.LogTime = DateTime.UtcNow;
        }

        public string LogId { get; set; }
        public string RoleName { get; set; }
        public string InstanceNumber { get; set; }
        public DateTime LogTime { get; set; }
        public string Level { get; set; }
        public string Name { get; set; }
        public string TraceId { get; set; }
        public string Service { get; set; }
        public string Tenant { get; set; }
        public string ServerOrigin { get; set; }
        public IReadOnlyCollection<KeyValuePair<string, object>> ListFields { get; set; }
        public string Environment { get; set; }
    }
}
