using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Retrospective.Infrastructure.Persistence;
using Serilog;
using System;

namespace Retrospective.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
                .Enrich.FromLogContext()
                .WriteTo.Console()
                .WriteTo.Seq("http://seq:5341")
                .CreateLogger();

            try
            {
                Log.Information("starting....");
                var host = CreateHostBuilder(args).Build();

                // using var scope = host.Services.CreateScope();
                // var ctx = scope.ServiceProvider.GetService<SprintContext>();
                // ctx.Database.Migrate();

                host.Run();
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "App start failed");
                throw;
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
