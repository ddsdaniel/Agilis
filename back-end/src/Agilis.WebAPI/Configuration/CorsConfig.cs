using Microsoft.AspNetCore.Builder;

namespace Agilis.WebAPI.Configuration
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
                    "http://localhost:4200",
                    "https://localhost:4200",
                    "http://localhost:52834",
                    "https://localhost:52834"
               };

            //TODO: restringir mais em produção
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
