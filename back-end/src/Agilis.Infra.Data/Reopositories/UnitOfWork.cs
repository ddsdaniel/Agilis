using MongoDB.Driver;
using Agilis.Domain.Abstractions.Repositories;
using Agilis.Domain.Abstractions.Repositories.Seguranca;
using Agilis.Infra.Data.Repositories.Seguranca;
using System;
using System.Threading.Tasks;

namespace Agilis.Infra.Data.Repositories
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly IClientSessionHandle _session;
        private bool _disposed = false;
        public IUsuarioRepository UsuarioRepository { get; }

        public UnitOfWork(IMongoDatabase database)
        {
            _session = database.Client.StartSession();
            _session.StartTransaction();

            UsuarioRepository = new UsuarioRepository(database, _session);
        }

        public async Task Commit()
        {
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
                if (_session.IsInTransaction)
                    _session.AbortTransaction();

                _session.Dispose();
            }

            _disposed = true;
        }
    }
}
