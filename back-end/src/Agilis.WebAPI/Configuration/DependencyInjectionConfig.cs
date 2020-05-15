﻿using DDS.Domain.Core.Abstractions.Services.Seguranca.Criptografia;
using DDS.Domain.Core.Services.Seguranca.Criptografia;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Agilis.Domain.Abstractions.Repositories;
using Agilis.Domain.Abstractions.Services.Seguranca;
using Agilis.Domain.Abstractions.ValueObjects;
using Agilis.Infra.Data.Repositories;
using Agilis.Domain.Models.ValueObjects;
using Agilis.Domain.Services.Seguranca;
using Agilis.Domain.Abstractions.Services.Pessoas;
using Agilis.Domain.Abstractions.Repositories.Pessoas;
using Agilis.Infra.Data.Reopositories.Pessoas;
using Agilis.Domain.Abstractions.Services.Trabalho;
using Agilis.Domain.Abstractions.Repositories.Trabalho;
using Agilis.Domain.Services.Trabalho;
using Agilis.Infra.Data.Reopositories.Trabalho;
using Agilis.Domain.Services.Pessoas;
using System;
using Agilis.Domain.Abstractions.Entities.Pessoas;
using DDS.Domain.Core.Model.ValueObjects;

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
            var mongoDatabase = new MongoClient(new MongoClientSettings { ReplicaSetName = "rs1" }).GetDatabase("agilis");
            services.TryAddScoped(x => mongoDatabase);

            services.AddScoped<IUnitOfWork, UnitOfWork>();

            //Pessoas
            services.AddScoped<IUsuarioService, UsuarioService>();
            services.AddScoped<IUsuarioRepository, UsuarioRepository>();

			services.AddScoped<IProdutoService, ProdutoService>();
            services.AddScoped<IProdutoRepository, ProdutoRepository>();

			services.AddScoped<ITimeService, TimeService>();
            services.AddScoped<ITimeRepository, TimeRepository>();

			services.AddScoped<IAtorService, AtorService>();
            services.AddScoped<IAtorRepository, AtorRepository>();

            //Seguranca
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<ICriptografiaSimetrica, AdvancedEncryptionStandard>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped(serviceProvider => ObterUsuarioLogado(serviceProvider));

            //Trabalho
            services.AddScoped<IUserStoryService, UserStoryService>();
            services.AddScoped<IUserStoryRepository, UserStoryRepository>();

            services.AddScoped<IMilestoneService, MilestoneService>();
            services.AddScoped<IMilestoneRepository, MilestoneRepository>();

            //AppSettings
            services.Configure<AppSettings>(configuration.GetSection(nameof(AppSettings)));
            services.AddSingleton<IAppSettings>(serviceProvider => serviceProvider.GetRequiredService<IOptions<AppSettings>>().Value);

            return services;
        }

        private static IUsuario ObterUsuarioLogado(IServiceProvider serviceProvider)
        {
            var httpContextAccessor = serviceProvider.GetService<IHttpContextAccessor>();
            var email = new Email(httpContextAccessor.HttpContext.User.Identity.Name);

            var scope = serviceProvider.CreateScope();
            var usuarioRepository = scope.ServiceProvider.GetRequiredService<IUsuarioRepository>();

            var usuario = usuarioRepository.ConsultarPorEmail(email);

            scope.Dispose();

            return usuario;
        }
    }
}
