namespace Playbook.Common.Instrumentation
{
    using System;
    using System.Collections.Generic;

    public sealed class InstrumentationSerilogManager : IInstrumentationLogManager
    {
        /// <summary>
        /// The base log file sink.
        /// </summary>
        private static IInstrumentationLogSink logSink;

        /// <summary>
        /// The singleton instance of this class.
        /// </summary>
        private static InstrumentationSerilogManager instance;

        /// <summary>
        /// The multi thread sync lock.
        /// </summary>
        private static object syncLock = new object();

        private InstrumentationSerilogManager()
        {
            logSink = new InstrumentationSerilogSink();
        }

        public static InstrumentationSerilogManager Current
        {
            get
            {
                if (instance == null)
                {
                    lock (syncLock)
                    {
                        if (instance == null)
                        {
                            instance = new InstrumentationSerilogManager();
                        }
                    }
                }

                return instance;
            }
        }

        public void LogMessage(string name, InstrumentationContext context, InstrumentationLevel level, string description = null)
        {
            InstrumentationArgs dataFields = null;
            if (description != null)
            {
                dataFields = new InstrumentationArgs { { "Message", description } };
            }

            this.LogMessage(name, context, level, dataFields);
        }

        public void LogMessage(string name, InstrumentationContext context, InstrumentationLevel level, IReadOnlyCollection<KeyValuePair<string, object>> dataFields)
        {
            // write to log file sink
            logSink.LogMessage(name, context, level, dataFields);
        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if (disposing)
            {
                // dispose log file sink
                if (logSink != null)
                {
                    logSink.Dispose();
                    logSink = null;
                }
            }
        }

    }
}
