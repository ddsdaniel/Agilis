using Agilis.Core.Domain.Abstractions.UnitsOfWork;
using Agilis.Core.Domain.Models.ValueObjects;

namespace Agilis.Core.Domain.Abstractions.Factories
{
    public interface IUnitOfWorkFactory
    {
        IUnitOfWorkInquilino ObterUnitOfWorkInquilino(Email email);
        IUnitOfWorkCatalogo ObterUnitOfWorkCatalogo();
    }
}
