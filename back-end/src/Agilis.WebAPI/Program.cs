using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace Agilis.WebAPI
{
    /// <summary>
    /// Classe de inicializa��o da aplica��o
    /// </summary>
    public class Program
    {
        /// <summary>
        /// M�todo por onde a aplica��o inicia
        /// </summary>
        /// <param name="args">Par�metros opcionais passados para a inicializa��o da aplica��o</param>
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        /// <summary>
        /// Cria um builder default e configura o uso da Startup
        /// </summary>
        /// <param name="args">Lista de par�metros</param>
        /// <returns>IHostBuilder</returns>
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
