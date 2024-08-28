namespace Playbook.Common.Instrumentation
{
    using System;
    using System.Collections.Generic;

    public interface IInstrumentationLogSink : IDisposable
    {
        void LogMessage(string name, InstrumentationContext context, InstrumentationLevel level, IReadOnlyCollection<KeyValuePair<string, object>> dataFields = null);

    }
}
