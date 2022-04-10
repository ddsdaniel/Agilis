using Agilis.Core.Domain.Abstractions.Models.Entities;
using Agilis.Core.Domain.Abstractions.Repositories;
using Agilis.Core.Domain.Abstractions.UnitsOfWork;
using System.Collections;
using System.Reflection;

namespace Agilis.Infra.Data.SqlServer.UnitsOfWork
{
    public class UnitOfWorkSqlServer : IUnitOfWork
    {
        private readonly AgilisDbContext _agilisDbContext;
        private IList _repositories;
        private bool _disposed = false;

        public UnitOfWorkSqlServer(AgilisDbContext agilisDbContext)
        {
            _repositories = new ArrayList();
            _agilisDbContext = agilisDbContext;
        }

        public Task AbortTransactionAsync()
        {
            //TODO: AbortTransactionAsync
            throw new NotImplementedException();
        }

        public Task CommitAsync()
        {
            return _agilisDbContext.SaveChangesAsync();
        }

        public IRepository<TEntity> ObterRepository<TEntity>()
            where TEntity : Entidade
        {
            var retorno = _repositories.OfType<IRepository<TEntity>>().SingleOrDefault();

            if (retorno == null)
            {
                var assembliesString = new string[]
                {
                    "Agilis.Infra.Data.SqlServer"
                };

                var allTypes = new List<Type>();
                foreach (var assemblyString in assembliesString)
                {
                    allTypes.AddRange(Assembly.Load(assemblyString).GetExportedTypes());
                }

                Type[] repositoriosImplementados = (from t in allTypes
                                                    where !t.IsInterface && !t.IsAbstract
                                                    where typeof(IRepository<TEntity>).IsAssignableFrom(t)
                                                    select t)
                                                    .ToArray();

                if (repositoriosImplementados.Length > 0)
                {
                    IRepository<TEntity>[] instantiatedTypes = repositoriosImplementados
                        .Select(typeRepo => (IRepository<TEntity>)Activator.CreateInstance(typeRepo, _agilisDbContext))
                        .ToArray();

                    retorno = instantiatedTypes.SingleOrDefault();
                    _repositories.Add(retorno);
                }
            }

            return retorno;
        }
        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _agilisDbContext.Dispose();
                }
            }
            _disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
