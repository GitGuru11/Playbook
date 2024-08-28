namespace Playbook.Common.Tenant
{
    using System;

    public class TenantContext : MarshalByRefObject
    {

        internal const string TenantContextKey = "TenantContextKey";

        public TenantContext(string tenantName)
        {
            if (string.IsNullOrEmpty(tenantName))
                throw new ArgumentNullException(nameof(tenantName));
            this.TenantName = tenantName;
        }

        public string TenantName { get; set; }


        public static TenantContext Current
        {
            get
            {
                var context = TenantCallContext<TenantContext>.GetData(TenantContextKey);
                return context;
            }

            protected set
            {
                if (value != null)
                {
                    TenantCallContext<TenantContext>.SetData(TenantContextKey, value);
                }
                else
                {
                    TenantCallContext<TenantContext>.FreeNamedDataSlot(TenantContextKey);
                }

            }
        }

        public TenantContext AsCurrent()
        {
            Current = this;
            return this;
        }
    }
}
