using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

using Serilog;
using Serilog.Events;
using Serilog.Exceptions;
using Serilog.Formatting.Compact;

namespace Travel.WebApi
{
    public class Program
    {
        public static int Main(string[] args)
        {
            var name = Assembly.GetExecutingAssembly().GetName();
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .Enrich.FromLogContext()
                .Enrich.WithExceptionDetails()
                .Enrich.WithMachineName()
                .Enrich.WithProperty("Assembly", $"{name.Name}")
                .Enrich.WithProperty("Assembly", $"${name.Version}")
                .WriteTo.SQLite(
                    Environment.CurrentDirectory + @"/Logs/log.db",
                    restrictedToMinimumLevel: LogEventLevel.Information,
                    storeTimestampInUtc: true
                    )
                .WriteTo.File(
                    new CompactJsonFormatter(),
                    Environment.CurrentDirectory + @"/Logs/log.json",
                    rollingInterval: RollingInterval.Day,
                    restrictedToMinimumLevel: LogEventLevel.Information
                )
                .WriteTo.Console()
                .CreateLogger();

            try
            {
                Log.Information("Starting host");
                CreateHostBuilder(args).Build().Run();
                return 0;
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Host terminated unexpectedly");
                return 1;
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .UseSerilog()
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
