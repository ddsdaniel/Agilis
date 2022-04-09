using Agilis.Core.Domain.Abstractions.Models.Entities;
using Agilis.Core.Domain.Abstractions.Repositories;
using System;
using System.Threading.Tasks;

namespace Agilis.Core.Domain.Abstractions.UnitsOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<TEntity> ObterRepository<TEntity>()
            where TEntity : Entidade;

        Task AbortTransactionAsync();
        Task CommitAsync();
    }
}
