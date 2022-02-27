using Moq;
using Moq.AutoMock;
using Agilis.Core.Domain.Abstractions.Models.Entities;
using Agilis.Core.Domain.Abstractions.Repositories;
using Agilis.Core.Domain.Abstractions.UnitsOfWork;

namespace Agilis.Test.Integration.Extensions
{
    public static class AutoMockerExtensions
    {
        public static Mock<IRepository<TEntity>> MockarRepositoryInquilino<TEntity>(this AutoMocker source)
           where TEntity : Entidade
        {
            var repository = new Mock<IRepository<TEntity>>();

            source.GetMock<IUnitOfWorkInquilino>()
                .Setup(u => u.ObterRepository<TEntity>())
                .Returns(repository.Object);

            return repository;
        }

        public static Mock<IRepository<TEntity>> MockarRepositoryCatalogo<TEntity>(this AutoMocker source)
            where TEntity : Entidade
        {
            var repository = new Mock<IRepository<TEntity>>();

            source.GetMock<IUnitOfWorkCatalogo>()
                .Setup(u => u.ObterRepository<TEntity>())
                .Returns(repository.Object);

            return repository;
        }
    }
}
