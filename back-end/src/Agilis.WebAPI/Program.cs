using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace Agilis.WebAPI
{
    /// <summary>
    /// Classe de inicialização da aplicação
    /// </summary>
    public class Program
    {
        /// <summary>
        /// Método por onde a aplicação inicia
        /// </summary>
        /// <param name="args">Parâmetros opcionais passados para a inicialização da aplicação</param>
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        /// <summary>
        /// Cria um builder default e configura o uso da Startup
        /// </summary>
        /// <param name="args">Lista de parâmetros</param>
        /// <returns>IHostBuilder</returns>
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
