using DDS.Domain.Core.Abstractions.Services.Seguranca.Criptografia;
using DDS.Domain.Core.Services.Seguranca.Criptografia;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Agilis.Domain.Abstractions.Repositories;
using Agilis.Domain.Abstractions.Repositories.Seguranca;
using Agilis.Domain.Abstractions.Services.Seguranca;
using Agilis.Domain.Abstractions.ValueObjects;
using Agilis.Infra.Data.Repositories;
using Agilis.Infra.Data.Repositories.Seguranca;
using Agilis.Domain.Models.ValueObjects;
using Agilis.Domain.Services.Seguranca;

namespace Agilis.WebAPI.Configuration
{
    /// <summary>
    /// Classe de configuração das injeções de dependências
    /// </summary>
    public static class DependencyInjectionConfig
    {
        /// <summary>
        /// Configura as injeções de dependências
        /// </summary>
        /// <param name="services">Coleção de serviços da startup</param>
        /// <param name="configuration">Propriedades de configuração da aplicação</param>
        /// <returns>services atualizado</returns>
        public static IServiceCollection AddDependencyInjectionConfig(this IServiceCollection services, IConfiguration configuration)
        {
            //Banco de dados
            var mongoDatabase = new MongoClient(new MongoClientSettings { ReplicaSetName = "rs1" }).GetDatabase("Agilis");
            services.TryAddScoped(x => mongoDatabase);

            services.AddScoped<IUnitOfWork, UnitOfWork>();

            //Seguranca
            services.AddScoped<IUsuarioRepository, UsuarioRepository>();
            services.AddScoped<IUsuarioService, UsuarioService>();
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<ICriptografiaSimetrica, AdvancedEncryptionStandard>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            //AppSettings
            services.Configure<AppSettings>(configuration.GetSection(nameof(AppSettings)));
            services.AddSingleton<IAppSettings>(serviceProvider => serviceProvider.GetRequiredService<IOptions<AppSettings>>().Value);

            return services;
        }
    }
}
