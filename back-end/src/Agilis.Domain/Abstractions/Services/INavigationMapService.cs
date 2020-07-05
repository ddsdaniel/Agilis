using Agilis.Domain.Abstractions.Entities;
using DDS.Domain.Core.Abstractions.Services;

namespace Agilis.Domain.Abstractions.Services
{
    public interface INavigationMapService : IService
    {
        EntidadeNodo Obter();
    }
}
