using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using MongoDB.Bson.Serialization;
using Agilis.Infra.Data.Configuration.Providers;
using System;
using DDS.Domain.Core.Abstractions.Services.Seguranca.Criptografia;
using Agilis.Domain.Abstractions.ValueObjects;

namespace Agilis.WebAPI.Configuration
{
    /// <summary>
    /// Define as configurações globais para o mongo
    /// </summary>
    public static class MongoConfig
    {
        /// <summary>
        /// Configura os parâmetros globais para o mongo, como por exemplo: serialização especial para determinados tipos de dados
        /// </summary>
        /// <param name="app">Classe que provê os mecanismos para configurar os serviços</param>
        /// <param name="serviceProvider">Usado para obter os serviços necessários</param>
        /// <returns>App pós configuração</returns>
        public static IApplicationBuilder UseMongoConfig(this IApplicationBuilder app, IServiceProvider serviceProvider)
        {
            var criptografiaSimetrica = serviceProvider.GetService<ICriptografiaSimetrica>();
            var appSettings = serviceProvider.GetService<IAppSettings>();            

            BsonSerializer.RegisterSerializationProvider(new DomainProvider(criptografiaSimetrica, appSettings));

            return app;
        }
    }
}
