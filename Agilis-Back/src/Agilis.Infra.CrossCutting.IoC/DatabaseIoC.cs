using Agilis.Core.Domain.Abstractions.Models.ValueObjects;
using Agilis.Core.Domain.Abstractions.Services;
using Agilis.Core.Domain.Abstractions.UnitsOfWork;
using Agilis.Infra.CrossCutting.IoC.Extensions;
using Agilis.Infra.Data.Mongo.Providers;
using Agilis.Infra.Data.Mongo.UnitsOfWork;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Driver;
using System;

namespace Agilis.Infra.CrossCutting.IoC
{
    public static class DatabaseIoC
    {
        public static IServiceCollection AddDatabaseIoC(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped(serviceProvider => ObterUnitOfWork(services));

            ConfigurarSerializations(services);

            return services;
        }

        private static IUnitOfWork ObterUnitOfWork(IServiceCollection services)
        {
            var connectionString = ObterConnectionString(services);

            var catalogoDatabase = new MongoClient(connectionString)
               .GetDatabase("agilis");

            return new MongoUnitOfWork(catalogoDatabase);
        }

        private static string ObterConnectionString(IServiceCollection services)
        {
            //TODO: proteger connection string
            var appSettings = services.Get<IAppSettings>();
            return appSettings.BancoDados.ConnectionString;
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

            BsonSerializer.RegisterSerializationProvider(new DomainProvider(criptografiaSimetrica));

            BsonClassMap.RegisterClassMap<EventContainer>(cm =>
            {
                cm.AutoMap();
                cm.UnmapMember(m => m.Eventos);
            });
        }
    }
}
