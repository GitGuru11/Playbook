namespace Playbook.Common.Instrumentation
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;


    [Serializable]
    public class InstrumentationArgs : Dictionary<string, object>
    {
        public InstrumentationArgs()
        {
        }

        public InstrumentationArgs(params object[] args)
        {
            if (args != null)
            {
                for (int key = 0; key < args.Length; key++)
                {
                    this.Add(key.ToString(CultureInfo.InvariantCulture), args[key]);
                }
            }
        }
    }
}