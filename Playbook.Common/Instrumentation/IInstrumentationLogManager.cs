namespace Playbook.Common.Instrumentation
{
    using System;
    using System.Collections.Generic;

    public interface IInstrumentationLogManager : IDisposable
    {
        void LogMessage(string name, InstrumentationContext context, InstrumentationLevel level, string description = null);

        void LogMessage(string name, InstrumentationContext context, InstrumentationLevel level, IReadOnlyCollection<KeyValuePair<string, object>> dataFields);

    }
}
