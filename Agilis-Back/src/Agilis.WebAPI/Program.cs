using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using System;
using DDS.Logs.Extensions;

namespace Agilis.WebAPI
{
    public class Program
    {
        public static int Main(string[] args)
        {
            try
            {
                var hostBuilder = CreateHostBuilder(args).Build();
                Serilog.Log.Information("Iniciando Web Host");
                hostBuilder.Run();
                return 0;
            }
            catch (Exception ex)
            {
                Serilog.Log.Fatal(ex, "Host encerrado inesperadamente");
                return 1;
            }
            finally
            {
                Serilog.Log.CloseAndFlush();
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureLogs()
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
