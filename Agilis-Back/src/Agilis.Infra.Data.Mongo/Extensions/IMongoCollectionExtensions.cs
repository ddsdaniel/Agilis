using MongoDB.Bson;
using MongoDB.Driver;

namespace Agilis.Infra.Data.Mongo.Extensions
{
    //TODO: extrair para uma lib
    public static class IMongoCollectionExtensions
    {
        public static void AddField(this IMongoCollection<BsonDocument> collection, string name, string defaultValue)
        {
            var update = Builders<BsonDocument>.Update.Set(name, defaultValue);
            var filter = Builders<BsonDocument>.Filter.Empty;
            var options = new UpdateOptions { IsUpsert = true };
            collection.UpdateMany(filter, update, options);
        }

        public static void DropField(this IMongoCollection<BsonDocument> collection, string name)
        {
            var update = Builders<BsonDocument>.Update.Unset(name);
            var filter = Builders<BsonDocument>.Filter.Empty;
            collection.UpdateMany(filter, update);
        }
    }
}
