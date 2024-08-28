using Microsoft.Extensions.Configuration;
using Serilog;

namespace Playbook.Common.Instrumentation
{
    public static class SeriLogManager
    {
        public static ILogger Logger { get; private set; }

        public static ILogger ConfigureSeriLog(IConfiguration configuration)
        {

            if (Logger == null)
            {
                Serilog.Debugging.SelfLog.Enable(Console.WriteLine);

                // assigning to Log.Logger is important.
                Logger = Log.Logger = new LoggerConfiguration()
                    .ReadFrom.Configuration(configuration)
                    .Enrich.FromLogContext()
                    .CreateLogger();
            }
            return Logger;
        }


    }
}
