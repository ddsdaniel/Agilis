using Microsoft.Extensions.DependencyInjection;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using System;
using Agilis.Core.Domain.Abstractions.Services;
using Agilis.Infra.Data.Mongo.Providers;
using Agilis.Core.Domain.Abstractions.Models.ValueObjects;
using Agilis.Core.Domain.Abstractions.UnitsOfWork;
using Agilis.Core.Domain.Abstractions.Factories;
using Agilis.Infra.Data.Mongo.Factories;
using Agilis.Infra.Data.Mongo.Services;
using Agilis.Infra.Seguranca.Abstractions.Models.Entities;
using Agilis.Infra.Configuracoes.Abstractions.Models.ValueObjects;

namespace Agilis.Infra.CrossCutting.IoC
{
    public static class DatabaseIoC
    {

        public static IServiceCollection AddDatabaseIoC(this IServiceCollection services)
        {
            services.AddScoped<IAdminDatabaseService, AdminDatabaseService>();
            services.AddScoped<IUnitOfWorkFactory, MongoUnitOfWorkFactory>();
            services.AddScoped(serviceProvider => ObterUnitOfWorkCatalogo(serviceProvider));
            services.AddScoped(serviceProvider => ObterUnitOfWorkInquilino(serviceProvider));
            ConfigurarSerializations(services);
            return services;
        }

        private static IUnitOfWorkCatalogo ObterUnitOfWorkCatalogo(IServiceProvider serviceProvider)
        {
            var unitOfWorkFactory = serviceProvider.GetService<IUnitOfWorkFactory>();
            return unitOfWorkFactory.ObterUnitOfWorkCatalogo();
        }

        private static IUnitOfWorkInquilino ObterUnitOfWorkInquilino(IServiceProvider serviceProvider)
        {
            var unitOfWorkFactory = serviceProvider.GetService<IUnitOfWorkFactory>();
            var usuarioLogado = serviceProvider.GetService<IUsuario>();

            if (usuarioLogado == null)
                throw new Exception("Usuário logado não foi encontrado.");

            return unitOfWorkFactory.ObterUnitOfWorkInquilino(usuarioLogado.Email);
        }

        private static void ConfigurarSerializations(IServiceCollection services)
        {
            // Torna a representação dos Guid do Mongo para ser igual do C#
            //BsonSerializer.RegisterSerializer(typeof(Guid), new GuidSerializer(GuidRepresentation.Standard));

            // Evita problema com o time zone ao gravar e recuperar dados
            BsonSerializer.RegisterSerializer(typeof(DateTime), new DateTimeSerializer(DateTimeKind.Local));

            // Regra para serialização de decimais
            BsonSerializer.RegisterSerializer(typeof(decimal), new DecimalSerializer(BsonType.Decimal128));

            var serviceProvider = services.BuildServiceProvider();

            var criptografiaSimetrica = serviceProvider.GetService<ICriptografiaSimetrica>();
            var appSettings = serviceProvider.GetService<IAppSettings>();

            BsonSerializer.RegisterSerializationProvider(new DomainProvider(criptografiaSimetrica, appSettings.Segredo));

            BsonClassMap.RegisterClassMap<EventContainer>(cm =>
            {
                cm.AutoMap();
                cm.UnmapMember(m => m.Eventos);
            });
        }
    }
}
