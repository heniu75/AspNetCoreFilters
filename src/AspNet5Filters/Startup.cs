﻿using AspNet5.Filters.ActionFilters;
using AspNet5.Filters.ExceptionFilters;
using AspNet5.Filters.ResourceFilters;
using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Hosting;
using Microsoft.Framework.DependencyInjection;
using Microsoft.Framework.Logging;

namespace AspNet5
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
        }

        public void ConfigureServices(IServiceCollection services)
        {
            var loggerFactory = new LoggerFactory { MinimumLevel = LogLevel.Debug };
            loggerFactory.AddConsole();
            loggerFactory.AddDebug();

            services.AddMvc(
                config =>
                    {
                        config.Filters.Add(new GlobalFilter(loggerFactory));
                        config.Filters.Add(new GlobalLoggingExceptionFilter(loggerFactory));
                    });

            services.AddScoped<ConsoleLogActionOneFilter>();
            services.AddScoped<ConsoleLogActionTwoFilter>();
            services.AddScoped<ClassConsoleLogActionBaseFilter>();
            services.AddScoped<ClassConsoleLogActionOneFilter>();

            services.AddScoped<CustomOneLoggingExceptionFilter>();
            services.AddScoped<CustomTwoLoggingExceptionFilter>();
            services.AddScoped<CustomOneResourceFilter>();   
        }

        // Configure is called after ConfigureServices is called.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.MinimumLevel = LogLevel.Debug;
            loggerFactory.AddConsole();
            loggerFactory.AddDebug();

            app.UseStaticFiles();
            app.UseMvc();
        }
    }
}
