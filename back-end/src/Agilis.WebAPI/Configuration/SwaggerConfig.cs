using System;
using System.IO;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.PlatformAbstractions;
using Microsoft.OpenApi.Models;

namespace Agilis.WebAPI.Configuration
{
    /// <summary>
    /// Classe de extensões para configurações do Swagger
    /// </summary>
    public static class SwaggerConfig
    {
        /// <summary>
        /// Configura os dados de Open Api Info, versão e caminho do XML
        /// </summary>
        /// <param name="services">Specifies the contract for a collection of service descriptors</param>
        /// <returns>services atualizado</returns>
        public static IServiceCollection AddSwaggerConfig(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1",
                     new OpenApiInfo()
                     {
                         Title = "API - Agilis",
                         Description = "Planejamento, organização, controle e análise de tarefas sobre uma metodologia ágil",
                         Version = PlatformServices.Default.Application.ApplicationVersion,
                         Contact = new OpenApiContact() { Name = "Daniel Dorneles da Silva", Email = "dds.daniel@gmail.com" },
                         License = new OpenApiLicense() { Name = "MIT", Url = new Uri("https://opensource.org/licenses/MIT") }
                     });

                string caminhoAplicacao = PlatformServices.Default.Application.ApplicationBasePath;
                string nomeAplicacao = PlatformServices.Default.Application.ApplicationName;
                string caminhoXmlDoc = Path.Combine(caminhoAplicacao, $"{nomeAplicacao}.xml");

                c.IncludeXmlComments(caminhoXmlDoc);
            });

            return services;
        }

        /// <summary>
        /// Habilita o uso do Swagger e define o seu Endpoint
        /// </summary>
        /// <param name="app">Defines a class that provides the mechanisms to configure an application's request pipeline</param>
        /// <returns>app atualizado</returns>
        public static IApplicationBuilder UseSwaggerConfig(this IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Agilis");
            });

            return app;
        }
    }
}
