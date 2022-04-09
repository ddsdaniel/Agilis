using Agilis.Infra.Data.Mongo.Extensions;
using Agilis.Core.Domain.Abstractions.Models.Entities;
using Agilis.Core.Domain.Abstractions.Repositories;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Linq.Expressions;
using MongoDB.Bson;

namespace Agilis.Infra.Data.Mongo.Abstractions.Repositories
{
    public class MongoRepository<TEntity> : IRepository<TEntity>
        where TEntity : Entidade
    {
        private readonly IMongoCollection<TEntity> _mongoCollection;
        private readonly IMongoDatabase _mongoDatabase;
        private readonly IClientSessionHandle _session;
        private readonly IMongoClient _mongoClient;
        private readonly string _collecionName;        

        public MongoRepository(IMongoDatabase mongoDatabase, IClientSessionHandle session)
        {
            _collecionName = typeof(TEntity).Name;
            _mongoClient = mongoDatabase.Client;

            var collectionNames = mongoDatabase.ListCollectionNames().ToList();
            
            CriarColecaoSeNaoExistir(mongoDatabase, collectionNames);
            _mongoDatabase = mongoDatabase;
            _session = session ?? _mongoClient.StartSession();

            if (!_session.IsInTransaction)
                _session.StartTransaction();

            _mongoCollection = mongoDatabase.GetCollection<TEntity>(_collecionName);
        }

        private void CriarColecaoSeNaoExistir(IMongoDatabase mongoDatabase, List<string> collectionNames)
        {
            try
            {
                if (!collectionNames.Any(d => d == _collecionName))
                    mongoDatabase.CreateCollection(_collecionName);
            }
            catch(MongoCommandException ex)
            {
                if (!ex.Message.Contains("Collection already exists"))
                    throw;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task AdicionarAsync(TEntity entity)
        {
            await _mongoCollection.InsertOneAsync(_session, entity);
        }

        public async Task AlterarAsync(TEntity entity)
        {
            var result = await _mongoCollection.ReplaceOneAsync(_session, e => e.Id == entity.Id, entity);
        }

        public async Task AlterarAsync<TField>(Expression<Func<TEntity, TField>> field, TField value, Expression<Func<TEntity, bool>> expression)
        {
            var filter = Builders<TEntity>.Filter.Where(expression);
            UpdateDefinition<TEntity> updateDefinition = Builders<TEntity>.Update.Set(field, value);
            await _mongoCollection.UpdateManyAsync(_session, filter, updateDefinition);
        }

        public async Task ExcluirAsync(Expression<Func<TEntity, bool>> expression)
        {
            var filter = Builders<TEntity>.Filter.Where(expression);
            await _mongoCollection.DeleteManyAsync(_session, filter);
        }

        public IQueryable<TEntity> Consultar()
        {
            return _mongoCollection.AsQueryable();
        }

        public bool ConsultarSeExiste(Guid id)
            => ConsultarPorIdAsync(id).Result != null;

        public async Task<TEntity> ConsultarPorIdAsync(Guid id)
        {
            return (await _mongoCollection.FindAsync(_session, entity => entity.Id == id)).FirstOrDefault();
        }

        public async Task ExcluirAsync(Guid id)
        {
            var entity = await ConsultarPorIdAsync(id);

            if (entity == null)
                return;

            var filter = Builders<TEntity>.Filter.Eq(doc => doc.Id, id);
            await _mongoCollection.FindOneAndDeleteAsync(_session, filter);
        }

        public async Task ExcluirVariosAsync(IEnumerable<TEntity> lista)
        {
            var filter = Builders<TEntity>.Filter.In("Id", lista.Select(x => x.Id));
            await _mongoCollection.DeleteManyAsync(filter);
        }

        public async Task AdicionarAsync(IEnumerable<TEntity> lista)
        {
            await _mongoCollection.InsertManyAsync(_session, lista);
        }

        public void ExcluirCampo(string nome)
        {
            var collection = _mongoDatabase.GetCollection<BsonDocument>(_collecionName);
            collection.DropField(nome);
        }
    }
}
