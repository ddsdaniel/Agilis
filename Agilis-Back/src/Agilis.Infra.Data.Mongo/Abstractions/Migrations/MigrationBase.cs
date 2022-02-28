using Microsoft.Extensions.Logging;
using Agilis.Core.Domain.Abstractions.UnitsOfWork;
using Agilis.Core.Domain.Models.Entities;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Agilis.Infra.Data.Mongo.Abstractions.Migrations
{
    public abstract class MigrationBase
    {
        private readonly string _nome;
        private readonly ILogger _logger;

        protected IUnitOfWork UnitOfWork { get; private set; }

        public MigrationBase(string nome, IUnitOfWork unitOfWork, ILogger logger)
        {
            _nome = nome;
            UnitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task Migrar()
        {
            try
            {
                var migrationRepository = UnitOfWork.ObterRepository<Migration>();

                if (!migrationRepository.Consultar().Any(m => m.Nome == _nome))
                {
                    _logger.LogInformation(_nome);
                    await MigrarAsync();
                    await migrationRepository.AdicionarAsync(new Migration(_nome));
                    await UnitOfWork.CommitAsync();
                }
            }
            catch (Exception erro)
            {
                _logger.LogError(erro.Message, erro);
            }
        }

        protected abstract Task MigrarAsync();
    }
}
