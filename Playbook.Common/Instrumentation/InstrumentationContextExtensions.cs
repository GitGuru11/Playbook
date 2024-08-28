namespace Playbook.Common.Instrumentation
{
    using System;
    using System.Globalization;
    using System.Linq;
    using System.Threading;

    /// <summary>
    /// The instrumentation context extensions.
    /// </summary>
    public static class InstrumentationContextExtensions
    {

        /// <summary>
        /// Gets a valid log manager for logging.
        /// </summary>
        private static IInstrumentationLogManager LogManager
        {
            get
            {
                IInstrumentationLogManager logManager = null;
                logManager = InstrumentationSerilogManager.Current;
                return logManager;
            }
        }

        public static void Log(this InstrumentationContext instrumentationContext, string name, InstrumentationLevel level, string messageFormat, InstrumentationArgs instrumentationArguments = null)
        {
            if (messageFormat == null)
            {
                // is string format sent in null or whitespace?  
                throw new ArgumentNullException("messageFormat");
            }
            else if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentNullException("name");
            }

            if (instrumentationArguments == null)
            {
                instrumentationArguments = new InstrumentationArgs();
            }

            if (instrumentationContext != null && instrumentationContext.IcId != InstrumentationContext.Empty.IcId)
            {
                instrumentationArguments["traceid"] = instrumentationContext.TraceId;
            }

            if (!string.IsNullOrWhiteSpace(messageFormat))
            {
                instrumentationArguments["message"] = messageFormat;
            }

            try
            {
                LogManager.LogMessage(
                         name,
                         instrumentationContext,
                         level,
                         instrumentationArguments);
            }
            catch (Exception ex)
            {
                var message = string.Format(
                    CultureInfo.InvariantCulture,
                    "InstrumentationContextExtensions.Log failed to log {0}: {1} {2}",
                    name,
                    ex,
                    instrumentationArguments);

                throw new Exception(message, ex);
            }
        }

        public static void Exception(this InstrumentationContext instrumentationContext, string name, InstrumentationLevel level, Exception exception, string messageFormat, InstrumentationArgs instrumentationArguments = null)
        {
            if (!string.IsNullOrEmpty(name) && exception != null)
            {
                instrumentationArguments = instrumentationArguments ?? new InstrumentationArgs();

                if (!instrumentationArguments.Keys.Any(key => string.Equals(key, "exception", StringComparison.OrdinalIgnoreCase)))
                {
                    instrumentationArguments["exception"] = exception.ToString();
                }

                if (string.IsNullOrEmpty(messageFormat))
                {
                    messageFormat = exception.ToString();
                }

                instrumentationContext.Log(name, level, messageFormat, instrumentationArguments);
            }
            else if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentNullException(nameof(name));
            }
            else if (exception == null)
            {
                throw new ArgumentNullException(nameof(exception));
            }
        }


        public static void Exception(this InstrumentationContext instrumentationContext, string name, InstrumentationLevel level, Exception exception, InstrumentationArgs instrumentationArguments = null)
        {
            Exception(instrumentationContext, name, level, exception, null, instrumentationArguments);
        }

        public static void Exception(this InstrumentationContext instrumentationContext, string name, Exception exception, InstrumentationArgs instrumentationArguments = null)
        {
            Exception(instrumentationContext, name, InstrumentationLevel.Exception, exception, null, instrumentationArguments);
        }


        public static void Exception(this InstrumentationContext instrumentationContext, string name, Exception exception, string messageFormat, InstrumentationArgs instrumentationArguments = null)
        {
            Exception(instrumentationContext, name, InstrumentationLevel.Exception, exception, messageFormat, instrumentationArguments);
        }

        public static void Error(this InstrumentationContext instrumentationContext, string name, string messageFormat, InstrumentationArgs instrumentationArguments = null)
        {
            Log(instrumentationContext, name, InstrumentationLevel.Error, messageFormat, instrumentationArguments);
        }

        public static void Warning(this InstrumentationContext instrumentationContext, string name, string messageFormat, InstrumentationArgs instrumentationArguments = null)
        {
            Log(instrumentationContext, name, InstrumentationLevel.Warning, messageFormat, instrumentationArguments);
        }
        public static void Fatal(this InstrumentationContext instrumentationContext, string name, string messageFormat, InstrumentationArgs instrumentationArguments = null)
        {
            Log(instrumentationContext, name, InstrumentationLevel.Fatal, messageFormat, instrumentationArguments);
        }

        public static void Important(this InstrumentationContext instrumentationContext, string name, string messageFormat, InstrumentationArgs instrumentationArguments = null)
        {
            Log(instrumentationContext, name, InstrumentationLevel.Important, messageFormat, instrumentationArguments);
        }

        public static void Information(this InstrumentationContext instrumentationContext, string name, string messageFormat, InstrumentationArgs instrumentationArguments = null)
        {
            Log(instrumentationContext, name, InstrumentationLevel.Information, messageFormat, instrumentationArguments);
        }

        public static void Debug(this InstrumentationContext instrumentationContext, string name, string messageFormat, InstrumentationArgs instrumentationArguments = null)
        {
            Log(instrumentationContext, name, InstrumentationLevel.Debug, messageFormat, instrumentationArguments);
        }


        // new methods
        public static void Information(this InstrumentationContext instrumentationContext, string messageFormat)
        {
            //Log(instrumentationContext, name, InstrumentationLevel.Information, messageFormat, instrumentationArguments);
        }

        public static void Information(this InstrumentationContext instrumentationContext, string messageFormat, params object?[]? propertyValues)
        {
            //Log(instrumentationContext, name, InstrumentationLevel.Information, messageFormat, instrumentationArguments);
        }

        public static void Information(this InstrumentationContext instrumentationContext, Exception? ex, string messageFormat, params object?[]? propertyValues)
        {
            //Log(instrumentationContext, name, InstrumentationLevel.Information, messageFormat, instrumentationArguments);
        }
    }


}
