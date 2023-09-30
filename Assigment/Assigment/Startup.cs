using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Assigment.Services;
using Swashbuckle.AspNetCore.Swagger;

using Newtonsoft.Json.Serialization;
using Assigment.Repositories;

namespace Assigment
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            Configuration = new ConfigurationBuilder().SetBasePath(env.ContentRootPath)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json")
                .AddEnvironmentVariables().Build();
        }

        public IConfiguration Configuration { get; }

        public static string Connectionstring { get; private set; }

        public static string LogTableName { get; private set; }

        public static string LogLevel { get; private set; }


        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            services.AddSingleton<IAssigmentRepository, AssigmentRepository>();
            services.AddSingleton<AssigmentService>();

            services.AddMvc().AddJsonOptions(options =>
            {
                options.SerializerSettings.ContractResolver = new DefaultContractResolver();
            });

            services.AddCors(c => c.AddPolicy("MyPolicy", builder =>
            {
                builder.AllowAnyOrigin()
                       .AllowAnyMethod()
                       .AllowAnyHeader();
            }));

            services.AddSwaggerGen(swagger =>
            {
                //This is to generate the Default UI of Swagger Documentation  
                swagger.SwaggerDoc("v1", new Info
                {
                    Version = "v1",
                    Title = " .Net Core Assigment API",
                    Description = "Assigment API"
                });
            });
            services.AddHttpClient();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();
            else
                app.UseHsts();

            Connectionstring = Configuration["ConnectionString:DefaultConnection"];
            LogTableName = Configuration["LogTableName"];
            LogLevel = Configuration["Logging:LogLevel:Default"];

            app.Use(async (context, next) =>
            {
                context.Response.Headers.Add("x-xss-protection", "1");
                await next();
            });

            app.UseAuthentication();
            app.UseCors("MyPolicy");
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                string swaggerJsonBasePath = string.IsNullOrWhiteSpace(c.RoutePrefix) ? "." : "..";
                c.SwaggerEndpoint($"{swaggerJsonBasePath}/swagger/v1/swagger.json", "AssigmentAPI");
            });
            app.UseMvc();
        }
    }
}
