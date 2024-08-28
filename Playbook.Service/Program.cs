using Playbook.Common.Instrumentation;
using Serilog;

namespace Playbook.Service
{
    public class Program
    {
        private static IConfiguration? Configuration;

        public static void Main(string[] args)
        {
            var env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            if (env == null)
            {
                throw new Exception("Environment variale ASPNETCORE_ENVIRONMENT is not set");
            }
            Configuration = new ConfigurationBuilder()
                             .SetBasePath(Directory.GetCurrentDirectory())
                             .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                             .AddJsonFile($"serilogsetting.json", optional: true, reloadOnChange: true)
                             .AddEnvironmentVariables()
                             .Build();


            // Serilog: setup the logger first to catch all errors
            var logger = SeriLogManager.ConfigureSeriLog(Configuration);

            try
            {
                logger.Information(string.Format("Starting Playbook Management Middleware, Ennvironment:{0}", env));
                var builder = CreateWebHostBuilder(args);
                var app = builder.Build();
                app.Run();
            }
            catch (Exception ex)
            {
                logger.Error("Stoped Playbook Management Middleware Host, Exception occurred: ", ex);
                throw;
            }

        }

        public static IHostBuilder CreateWebHostBuilder(string[] args)
        {
            var builder = Host.CreateDefaultBuilder(args)
               .ConfigureWebHostDefaults(webBuilder =>
               {
                   webBuilder.UseConfiguration(Configuration);
                   webBuilder.UseStartup<Startup>();

               });
            builder.UseSerilog();
            return builder;
        }
    }
}
