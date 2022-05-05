using Agilis.Core.Domain.Abstractions.Models.Entities;
using Agilis.Core.Domain.Abstractions.Repositories;
using Agilis.Core.Domain.Abstractions.UnitsOfWork;
using MongoDB.Driver;
using System.Collections;
using System.Reflection;

namespace Agilis.Infra.Data.Mongo.UnitsOfWork
{
    public class MongoUnitOfWork : IUnitOfWork
    {

        private readonly IClientSessionHandle _session;
        private readonly IMongoDatabase _database;
        private bool _disposed = false;
        private IList _repositories;

        public MongoUnitOfWork(IMongoDatabase database)
        {
            _session = database.Client.StartSession();
            _session.StartTransaction();

            _repositories = new ArrayList();
            _database = database;
        }

        public async Task AbortTransactionAsync()
        {
            if (_session.IsInTransaction)
                await _session.AbortTransactionAsync();
        }

        public async Task CommitAsync()
        {
            if (_session.IsInTransaction)
                await _session.CommitTransactionAsync();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (_disposed)
                return;

            if (disposing)
            {
                try
                {
                    if (_session.IsInTransaction)
                        _session.AbortTransaction();

                    _session.Dispose();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
            }

            _disposed = true;
        }

        public IRepository<TEntity> ObterRepository<TEntity>() 
            where TEntity : Entidade
        {
            var retorno = _repositories.OfType<IRepository<TEntity>>().SingleOrDefault();

            if (retorno == null)
            {

                var assembliesString = new string[]
                {
                    "Agilis.Infra.Data.Mongo"
                };

                var allTypes = new List<Type>();
                foreach (var assemblyString in assembliesString)
                {
                    allTypes.AddRange(Assembly.Load(assemblyString).GetExportedTypes());
                }

                Type[] repositoriosImplementados = (from t in allTypes
                                                    where !t.IsInterface && !t.IsAbstract
                                                    where typeof(IRepository<TEntity>).IsAssignableFrom(t)
                                                    select t).ToArray();

                if (repositoriosImplementados.Length > 0)
                {
                    IRepository<TEntity>[] instantiatedTypes = repositoriosImplementados
                        .Select(typeRepo => (IRepository<TEntity>)Activator.CreateInstance(typeRepo, _database, _session))
                        .ToArray();

                    retorno = instantiatedTypes.SingleOrDefault();
                    _repositories.Add(retorno);
                }
            }

            return retorno;
        }
    }
}
