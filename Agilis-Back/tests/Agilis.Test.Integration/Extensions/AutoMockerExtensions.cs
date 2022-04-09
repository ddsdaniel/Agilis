using Moq;
using Moq.AutoMock;
using Agilis.Core.Domain.Abstractions.Models.Entities;
using Agilis.Core.Domain.Abstractions.Repositories;
using Agilis.Core.Domain.Abstractions.UnitsOfWork;

namespace Agilis.Test.Integration.Extensions
{
    public static class AutoMockerExtensions
    {
        public static Mock<IRepository<TEntity>> MockarRepository<TEntity>(this AutoMocker source)
           where TEntity : Entidade
        {
            var repository = new Mock<IRepository<TEntity>>();

            source.GetMock<IUnitOfWork>()
                .Setup(u => u.ObterRepository<TEntity>())
                .Returns(repository.Object);

            return repository;
        }
    }
}
