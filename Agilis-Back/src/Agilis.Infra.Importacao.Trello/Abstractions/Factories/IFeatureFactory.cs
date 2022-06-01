using Agilis.Core.Domain.Models.Entities;
using TrelloSharpEasy.Entities;

namespace Agilis.Infra.Importacao.Trello.Abstractions.Factories
{
    public interface IFeatureFactory
    {
        Feature Criar(List list, Produto produto);
    }
}