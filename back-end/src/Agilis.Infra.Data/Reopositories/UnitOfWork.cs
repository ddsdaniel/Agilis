using MongoDB.Driver;
using Agilis.Domain.Abstractions.Repositories;
using System;
using System.Threading.Tasks;
using Agilis.Domain.Abstractions.Repositories.Pessoas;
using Agilis.Domain.Abstractions.Repositories.Trabalho;
using Agilis.Infra.Data.Reopositories.Pessoas;
using Agilis.Infra.Data.Reopositories.Trabalho;

namespace Agilis.Infra.Data.Repositories
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly IClientSessionHandle _session;
        private bool _disposed = false;
        public IUsuarioRepository UsuarioRepository { get; }
        public IUserStoryRepository UserStoryRepository { get; }
        public IAtorRepository AtorRepository { get; }
        public IMilestoneRepository MilestoneRepository { get; }

        public UnitOfWork(IMongoDatabase database)
        {
            _session = database.Client.StartSession();
            _session.StartTransaction();

            UsuarioRepository = new UsuarioRepository(database, _session);
            MilestoneRepository = new MilestoneRepository(database, _session);
            AtorRepository = new AtorRepository(database, _session);
            UserStoryRepository = new UserStoryRepository(database, _session);
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
