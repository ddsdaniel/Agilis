using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using Agilis.Core.Domain.Abstractions.Factories;
using Agilis.Core.Domain.Abstractions.UnitsOfWork;
using Agilis.Core.Domain.Extensions;
using Agilis.Core.Domain.Models.ValueObjects;
using Agilis.Infra.Data.Mongo.UnitsOfWork;
using System;

namespace Agilis.Infra.Data.Mongo.Factories
{
    public class MongoUnitOfWorkFactory : IUnitOfWorkFactory
    {
        private readonly string _connectionString;

        public MongoUnitOfWorkFactory(IConfiguration configuration)
        {
            _connectionString = configuration.GetValue<string>("ConnectionString");
        }

        public IUnitOfWorkCatalogo ObterUnitOfWorkCatalogo()
        {
            var catalogoDatabase = new MongoClient(_connectionString)
                .GetDatabase("agilis-catalogo");

            return new MongoUnitOfWorkCatalogo(catalogoDatabase);
        }

        public IUnitOfWorkInquilino ObterUnitOfWorkInquilino(Email email)
        {
            if (email == null)
                throw new Exception("Usuário não foi encontrado ao abrir conexão com o banco de dados.");

            var databaseName = $"agilis-{email.Endereco.ObterApenasLetras()}";
            
            var inquilinoDatabase = new MongoClient(_connectionString)
                .GetDatabase(databaseName);

            return new MongoUnitOfWorkInquilino(inquilinoDatabase);
        }
    }
}
