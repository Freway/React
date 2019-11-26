using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using BR.POINTER.LASTPOSITION.API.Helpers;
using Swashbuckle.AspNetCore.Swagger;
using System.IO;

namespace BR.POINTER.LASTPOSITION.API
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
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.AddCors(options =>
            {
                options.AddPolicy("AllowOrigin",
                        builder => builder.WithOrigins("http://localhost:3000", "https://connect-light.herokuapp.com")
                                          .WithMethods("GET", "POST", "OPTIONS", "HEAD")
                                          .AllowCredentials()
                                          .WithHeaders("accept", "content-type", "origin", 
                                                       "Access-Control-Allow-Headers", "Access-Control-Allow-Methods",
                                                       "Access-Control-Allow-Origin")
                                          .AllowAnyHeader()
                                          .SetPreflightMaxAge(TimeSpan.FromDays(7))

                );
            });

            services.AddSwaggerGen(c => {

                string applicationPath = AppDomain.CurrentDomain.BaseDirectory;
                string applicationName = AppDomain.CurrentDomain.FriendlyName;
                string xmlDocPath = Path.Combine(applicationPath, $"{applicationName}.xml");

                c.SwaggerDoc(AppSetting.Get("Swagger", "SwaggerVersion"), new Info
                {
                    Title = AppSetting.Get("Swagger", "SwaggerTitle"),
                    Version = AppSetting.Get("Swagger", "SwaggerVersion"),
                    Description = AppSetting.Get("Swagger", "SwaggerDescription"),
                    Contact = new Contact
                    {
                        Name = AppSetting.Get("Swagger", "SwaggerContactName"),
                        Email = AppSetting.Get("Swagger", "SwaggerContactEmail"),
                        Url = AppSetting.Get("Swagger", "SwaggerContactUrl")
                    }
                });

                //c.OperationFilter<TokenHeaderFilter>();

                c.IncludeXmlComments(xmlDocPath);
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors("AllowOrigin");

            app.UseMvc();

            app.UseSwagger();
            app.UseSwaggerUI(c => {
                c.RoutePrefix = AppSetting.Get("Swagger", "SwaggerRoutePrefix");
                c.SwaggerEndpoint($"{AppSetting.Get("Swagger", "SwaggerVersion")}/swagger.json", AppSetting.Get("Swagger", "SwaggerTitle"));
            });
        }
    }
}
