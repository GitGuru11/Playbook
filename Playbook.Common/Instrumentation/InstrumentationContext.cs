namespace Playbook.Common.Instrumentation
{
    using System;

    public class InstrumentationContext : MarshalByRefObject
    {

        internal const string InstrumentationLoggingContextKey = "InstrumentationLoggingContextKey";

        private static InstrumentationContext emptyContext = new InstrumentationContext(null, null, Guid.Empty);

        public InstrumentationContext()
            : this(string.Empty, string.Empty)
        {
        }

        public InstrumentationContext(string traceId, string serverOrigin)
            : this(traceId, serverOrigin, Guid.NewGuid())
        {
        }

        private InstrumentationContext(string traceId, string serverOrigin, Guid icid)
        {
            this.IcId = icid.ToString("N");
            this.TraceId = string.IsNullOrEmpty(traceId) ? this.IcId : traceId;
            this.ServerOrigin = serverOrigin;
        }

        internal static InstrumentationContext Empty
        {
            get
            {
                return emptyContext;
            }
        }

        public string IcId { get; set; }
        public string TraceId { get; set; }
        public string Operator { get; set; }
        public string Service { get; set; }
        public string Tenant { get; set; }
        public string Environment { get; set; }
        public string RoleName { get; set; }
        public string ApiName { get; set; }
        public string ServerOrigin { get; set; }
        public string InstanceNumber { get; set; }





        public static InstrumentationContext Current
        {
            get
            {
                var context = CallContext<InstrumentationContext>.GetData(InstrumentationLoggingContextKey);
                return context;
            }

            protected set
            {
                if (value != null)
                {
                    CallContext<InstrumentationContext>.SetData(InstrumentationLoggingContextKey, value);
                }
                else
                {
                    CallContext<InstrumentationContext>.FreeNamedDataSlot(InstrumentationLoggingContextKey);
                }

            }
        }

        public InstrumentationContext AsCurrent()
        {
            Current = this;
            return this;
        }
    }
}
