using Microsoft.OpenApi.Models;
using Playbook.Data.ClickHouse;
using Playbook.Instrumentation.Web;
using Playbook.Service.Filters;
using Playbook.Service.Middlewares;

namespace Playbook.Service
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc(options =>
            {
                options.Filters.Add<InstrumentationActionContextAttribute>();
                options.Filters.Add<TenantActionContextAttribute>();
            });

            services.AddOptions();
            var hostConfig = new HostConfiguration();
            Configuration.Bind("HostConfigurationSettings", hostConfig);

            // set ClickHouse connection string here
            DatabaseHandler.ConnectionString = Configuration.GetValue<string>("ClickHouseConnectionStrings");


            // todo: this could be security breach, need more contorlled approach here.
            services.AddCors(opt =>
            {
                opt.AddDefaultPolicy(builder =>
                {
                    builder.AllowAnyOrigin()
                        .AllowAnyHeader()
                        .AllowAnyMethod();
                });
            });


            services.AddControllers();
            services.AddApiVersioning(x =>
            {
                x.DefaultApiVersion = new Microsoft.AspNetCore.Mvc.ApiVersion(1, 0);
                x.AssumeDefaultVersionWhenUnspecified = true;
                x.ReportApiVersions = true;
            });

            services.AddHsts(options =>
            {
                options.Preload = true;
                options.IncludeSubDomains = true;
            });

            // Register the Swagger generator, defining 1 or more Swagger documents
            services.AddSwaggerGen(option =>
            {
                option.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Playbook Management Api",
                    Version = "1.0.0",
                    Description = "Playbook Management Api"
                });
                option.EnableAnnotations();
            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                // Enable middleware to serve generated Swagger as a JSON endpoint.

            }

            app.UseSwagger();
            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.), 
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                string swaggerJsonBasePath = string.IsNullOrWhiteSpace(c.RoutePrefix) ? "." : "..";
                c.SwaggerEndpoint($"{swaggerJsonBasePath}/swagger/v1/swagger.json", "Playbook 1.0.0");
            });

            app.UseCors(builder =>
            {
                builder.AllowAnyOrigin()
                    .AllowAnyHeader()
                    .AllowAnyMethod();
            });

            app.UseHsts();

            app.UseForwardedHeaders();
            app.UseCertificateForwarding();

            app.UseHttpsRedirection();

            app.UseStaticFiles();

            // for performance
            app.UseMiddleware(typeof(TimedOperationMiddleware));

            // global exception handler
            app.UseMiddleware(typeof(ErrorHandlingMiddleware));

            app.UseRouting();

            ////app.UseAuthentication();
            ////app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
