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
        public ITimeRepository TimeRepository { get; }
        public ISprintRepository SprintRepository { get; }
        public IMilestoneRepository MilestoneRepository { get; }
        public IReleaseRepository ReleaseRepository { get; }
        public IProdutoRepository ProdutoRepository { get; }
        public ITemaRepository TemaRepository { get; }
        public IAtorRepository AtorRepository { get; }
        public IEpicoRepository EpicoRepository { get; }

        public UnitOfWork(IMongoDatabase database)
        {
            _session = database.Client.StartSession();
            _session.StartTransaction();

            UsuarioRepository = new UsuarioRepository(database, _session);
            MilestoneRepository = new MilestoneRepository(database, _session);
            TimeRepository = new TimeRepository(database, _session);
            SprintRepository = new SprintRepository(database, _session);
            UserStoryRepository = new UserStoryRepository(database, _session);
            ReleaseRepository = new ReleaseRepository(database, _session);
            ProdutoRepository = new ProdutoRepository(database, _session);
            TemaRepository = new TemaRepository(database, _session);
            AtorRepository = new AtorRepository(database, _session);
            EpicoRepository = new EpicoRepository(database, _session);
        }

        public async Task Commit()
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
                catch(Exception e)
                {
                    Console.WriteLine(e);
                }
            }

            _disposed = true;
        }
    }
}
