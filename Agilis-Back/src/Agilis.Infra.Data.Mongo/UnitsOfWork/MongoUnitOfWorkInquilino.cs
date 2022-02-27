using MongoDB.Driver;
using Agilis.Core.Domain.Abstractions.UnitsOfWork;
using Agilis.Infra.Data.Mongo.Abstractions.UnitsOfWork;

namespace Agilis.Infra.Data.Mongo.UnitsOfWork
{
    public class MongoUnitOfWorkInquilino : MongoUnitOfWork, IUnitOfWorkInquilino
    {
        public MongoUnitOfWorkInquilino(IMongoDatabase database) : base(database)
        {
        }
    }
}
