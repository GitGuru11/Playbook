namespace Playbook.Common.Instrumentation
{
    using System;
    using System.Collections.Generic;
    using Serilog;

    /// <summary>
    /// An instrumentation sink that outputs to log output.
    /// </summary>
    public sealed class InstrumentationSerilogSink : IInstrumentationLogSink
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="InstrumentationSerilogSink" /> class.
        /// </summary>
        public InstrumentationSerilogSink()
        {
        }

        private InstrumentationLog CreateLoggingEntity(
            string logId,
            InstrumentationContext context,
            InstrumentationLevel level,
            IReadOnlyCollection<KeyValuePair<string, object>> listFields)
        {

            var entity = new InstrumentationLog();

            //InstrumentationDataUtils.SetCommonFields(entity);

            entity.Level = level.ToString();
            entity.Name = logId ?? string.Empty;
            entity.TraceId = context.TraceId;
            entity.InstanceNumber = context.InstanceNumber;
            entity.RoleName = context.RoleName;
            entity.Environment = context.Environment;
            entity.Tenant = context.Tenant;

            if (listFields != null)
            {
                // do not remove commented line below, based on the framework this is required.
                // entity.ListFields = InstrumentationDataUtils.SerializeListFieldsToJson(listFields);
                // entity.ListFields = JsonConvert.SerializeObject(listFields);
                entity.ListFields = listFields;
            }

            if (context != null)
            {
                entity.Service = context.Service;
                entity.ServerOrigin = context.ServerOrigin;
            }

            return entity;
        }

        private void Log(
            string logId,
            InstrumentationContext context,
            InstrumentationLevel level,
            IReadOnlyCollection<KeyValuePair<string, object>> listFields)
        {
            var logitem = this.CreateLoggingEntity(logId, context, @level, listFields);
            var logger = SeriLogManager.Logger.ForContext("Tenant", logitem.Tenant ?? string.Empty)
                .ForContext("RoleName", logitem.RoleName)
                .ForContext("TraceId", logitem.TraceId ?? string.Empty)
                .ForContext("ServerOrigin", logitem.ServerOrigin)
                .ForContext("Service", logitem.Service)
                .ForContext("Name", logitem.Name)
                .ForContext("Level", logitem.Level)
                .ForContext("Instance", logitem.InstanceNumber)
                .ForContext("RoleName", logitem.RoleName)
                .ForContext("Environment", logitem.Environment);
            foreach (var item in listFields.ToList())
            {
                logger = logger.ForContext(item.Key.ToString(), item.Value?.ToString());
            }


            switch (level)
            {
                case InstrumentationLevel.Verbose:
                    SeriLogManager.Logger.Verbose("{@InstrumentationLog}", logitem);
                    break;
                case InstrumentationLevel.Debug:
                    SeriLogManager.Logger.Debug("{@InstrumentationLog}", logitem);
                    break;
                case InstrumentationLevel.Warning:
                    SeriLogManager.Logger.Warning("{@InstrumentationLog}", logitem);
                    break;
                case InstrumentationLevel.Error:
                case InstrumentationLevel.Exception:
                    SeriLogManager.Logger.Error("{@InstrumentationLog}", logitem);
                    break;
                case InstrumentationLevel.Fatal:
                    SeriLogManager.Logger.Fatal("{@InstrumentationLog}", logitem);
                    break;
                default:
                    logger.Information("{@InstrumentationLog}", logitem);
                    break;
            }
        }

        public void LogMessage(
            string name,
            InstrumentationContext context,
            InstrumentationLevel level,
            IReadOnlyCollection<KeyValuePair<string, object>> dataFields = null)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentNullException("name");
            }

            this.Log(name, context, level, dataFields);
        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposalNative)
        {
        }

    }
}