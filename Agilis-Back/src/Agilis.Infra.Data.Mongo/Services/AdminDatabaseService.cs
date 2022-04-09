using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using Agilis.Core.Domain.Abstractions.Services;

namespace Agilis.Infra.Data.Mongo.Services
{
    public class AdminDatabaseService : IAdminDatabaseService
    {
        private readonly string _connectionString;

        public AdminDatabaseService(IConfiguration configuration)
        {
            _connectionString = configuration.GetValue<string>("ConnectionString");
        }

        public void DropDatabase(string name)
        {
            var mongoClient = new MongoClient(_connectionString);
            mongoClient.DropDatabase(name);
        }
    }
}
