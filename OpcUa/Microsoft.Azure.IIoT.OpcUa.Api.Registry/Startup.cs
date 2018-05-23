﻿// ------------------------------------------------------------
//  Copyright (c) Microsoft Corporation.  All rights reserved.
//  Licensed under the MIT License (MIT). See License.txt in the repo root for license information.
// ------------------------------------------------------------

namespace Microsoft.Azure.IIoT.OpcUa.Api.Registry {
    using Microsoft.Azure.IIoT.OpcUa.Api.Registry.v1;
    using Microsoft.Azure.IIoT.OpcUa.Api.Registry.Runtime;
    using Microsoft.Azure.IIoT.OpcUa.Services.Client;
    using Microsoft.Azure.IIoT.OpcUa.Services.Registry;
    using Microsoft.Azure.IIoT.OpcUa.Services.Protocol.Stack;
    using Microsoft.Azure.IIoT.Web.Auth;
    using Microsoft.Azure.IIoT.Diagnostics;
    using Microsoft.Azure.IIoT.Http;
    using Microsoft.Azure.IIoT.Hub.Client;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Logging;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Autofac;
    using Autofac.Extensions.DependencyInjection;
    using Newtonsoft.Json;
    using Swashbuckle.AspNetCore.Swagger;
    using System;
    using System.IO;
    using ILogger = Diagnostics.ILogger;

    /// <summary>
    /// Webservice startup
    /// </summary>
    public class Startup {

        /// <summary>
        /// Configuration - Initialized in constructor
        /// </summary>
        public Config Config { get; }

        /// <summary>
        /// Current hosting environment - Initialized in constructor
        /// </summary>
        public IHostingEnvironment Environment { get; }

        /// <summary>
        /// Di container - Initialized in `ConfigureServices`
        /// </summary>
        public IContainer ApplicationContainer { get; private set; }

        /// <summary>
        /// Created through builder
        /// </summary>
        /// <param name="env"></param>
        public Startup(IHostingEnvironment env) {
            Environment = env;

            var config = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile(
                    "appsettings.json", true, true)
                .AddJsonFile(
                    $"appsettings.{env.EnvironmentName}.json", true, true)
                .AddEnvironmentVariables()
                .Build();

            Config = new Config(config);
        }

        /// <summary>
        /// This is where you register dependencies, add services to the
        /// container. This method is called by the runtime, before the
        /// Configure method below.
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public IServiceProvider ConfigureServices(IServiceCollection services) {

            // Setup (not enabling yet) CORS
            services.AddCors();

            // Add authentication
            services.AddJwtBearerAuthentication(Config,
                Environment.IsDevelopment());

            // Add authorization
            services.AddAuthorization(options => {
                options.AddV1Policies(Config);
            });

            // Add controllers as services so they'll be resolved.
            services.AddMvc().AddControllersAsServices().AddJsonOptions(options => {
                options.SerializerSettings.Formatting = Formatting.Indented;
                options.SerializerSettings.Converters.Add(new ExceptionConverter(
                    Environment.IsDevelopment()));
                options.SerializerSettings.MaxDepth = 10;
            });

            // Generate swagger documentation
            services.AddSwaggerGen(options => {
                // Add info
                options.SwaggerDoc(ServiceInfo.PATH, new Info {
                    Title = ServiceInfo.NAME,
                    Version = ServiceInfo.PATH,
                    Description = ServiceInfo.DESCRIPTION,
                });

                // Add help
                options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory,
                    typeof(Startup).Assembly.GetName().Name + ".xml"));

                // If auth enabled, need to have bearer token to access any api
                if (Config.AuthRequired) {
                    options.AddSecurityDefinition("bearer", new ApiKeyScheme {
                        Description = "Authorization token in the form of 'bearer <token>'",
                        Name = "Authorization",
                        In = "header"
                    });
                }
            });

            // Prepare DI container
            ApplicationContainer = ConfigureContainer(services);
            // Create the IServiceProvider based on the container
            return new AutofacServiceProvider(ApplicationContainer);
        }


        /// <summary>
        /// This method is called by the runtime, after the ConfigureServices
        /// method above and used to add middleware
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        /// <param name="loggerFactory"></param>
        /// <param name="corsSetup"></param>
        /// <param name="appLifetime"></param>
        public void Configure(IApplicationBuilder app, IHostingEnvironment env,
            ILoggerFactory loggerFactory, ICorsSetup corsSetup,
            IApplicationLifetime appLifetime) {

            var log = ApplicationContainer.Resolve<ILogger>();
            loggerFactory.AddConsole(Config.Configuration.GetSection("Logging"));

            if (Config.AuthRequired) {
                // Try authenticate
                app.UseAuthentication();
                // Filter for x-source
                app.UseMiddleware<AuthMiddleware>();
            }

            // Enable CORS - Must be before UseMvc
            // see: https://docs.microsoft.com/en-us/aspnet/core/security/cors
            corsSetup.UseMiddleware(app);

            // Enable swagger and swagger ui
            app.UseSwagger(options => {
                options.PreSerializeFilters.Add((doc, request) =>
                    doc.Host = request.Host.Value);
            });
            app.UseSwaggerUI(options => {
                options.RoutePrefix = "";
                options.SwaggerEndpoint("/swagger/v1/swagger.json",
                    $"{ServiceInfo.PATH} Docs ({ServiceInfo.DATE})");
            });

            app.UseMvc();

            // If you want to dispose of resources that have been resolved in the
            // application container, register for the "ApplicationStopped" event.
            appLifetime.ApplicationStopped.Register(ApplicationContainer.Dispose);

            // Print some useful information at bootstrap time
            log.Info($"{ServiceInfo.NAME} web service started",
                () => new { Uptime.ProcessId, env });
        }

        /// <summary>
        /// Autofac configuration. Find more information here:
        /// @see http://docs.autofac.org/en/latest/integration/aspnetcore.html
        /// </summary>
        public IContainer ConfigureContainer(IServiceCollection services) {
            var builder = new ContainerBuilder();

            // Populate from services di
            builder.Populate(services);

            // By default Autofac uses a request lifetime, creating new objects
            // for each request, which is good to reduce the risk of memory
            // leaks, but not so good for the overall performance.

            // Register logger
            builder.RegisterInstance(Config.Logger)
                .AsImplementedInterfaces().SingleInstance();

            // Register configuration interfaces
            builder.RegisterInstance(Config)
                .AsImplementedInterfaces().SingleInstance();

            // Auth and CORS setup
            builder.RegisterType<CorsSetup>()
                .AsImplementedInterfaces().SingleInstance();

            // Register http client implementation
            builder.RegisterType<HttpClient>()
                .AsImplementedInterfaces().SingleInstance();

            // Register endpoint services and ...
            builder.RegisterType<OpcUaRegistryServices>()
                .AsImplementedInterfaces().SingleInstance();

            // Iot hub services
            builder.RegisterType<IoTHubServiceHttpClient>()
                .AsImplementedInterfaces().SingleInstance();

            // Validate only through jobs
            builder.RegisterType<OpcUaTwinValidator>()
                .AsImplementedInterfaces().SingleInstance();

            // Register composite client
            builder.RegisterType<OpcUaCompositeClient>()
                .AsImplementedInterfaces().SingleInstance();

            return builder.Build();
        }
    }
}
