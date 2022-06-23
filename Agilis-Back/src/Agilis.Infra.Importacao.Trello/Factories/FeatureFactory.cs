using Agilis.Core.Domain.Models.Entities;
using Agilis.Infra.Importacao.Trello.Abstractions.Factories;
using TrelloSharpEasy.Entities;

namespace Agilis.Infra.Importacao.Trello.Factories
{
    public class FeatureFactory : IFeatureFactory
    {
        public Feature Criar(List list, Produto produto)
        {
            return new Feature(
                     nome: list.Name,
                     produto
                 );
        }
    }
}
