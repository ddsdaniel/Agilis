using Microsoft.AspNetCore.Builder;

namespace Agilis.WebAPI.Configurations
{
    /// <summary>
    /// Classe de extensão para separar as configurações de Cors
    /// </summary>
    public static class CorsConfig
    {
        /// <summary>
        /// Configura o Cors
        /// </summary>
        /// <param name="app">Defines a class that provides the mechanisms to configure an application's request pipeline</param>
        /// <returns>app atualizado</returns>
        public static IApplicationBuilder UseCorsConfig(this IApplicationBuilder app)
        {
            var origens = new string[]
               {
                    "http://localhost",
                    "http://localhost:4200",
                    "https://localhost:4200",
                    "http://localhost:58068",
                    "https://localhost:58068",
                    "https://localhost:443",
                    "http://localhost:80",
                    "https://www.appAgilis.com:443",
                    "https://www.appAgilis.com",
					"https://appAgilis.com"
               };

            app.UseCors(x => x
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .WithOrigins(origens)
                .AllowCredentials()
                .AllowAnyHeader());

            return app;
        }
    }
}
