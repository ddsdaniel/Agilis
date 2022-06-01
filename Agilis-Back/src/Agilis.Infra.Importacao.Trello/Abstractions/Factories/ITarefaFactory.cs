using Agilis.Core.Domain.Models.Entities;
using Agilis.Core.Domain.Models.Entities.Tarefas;
using TrelloSharpEasy.Entities;

namespace Agilis.Infra.Importacao.Trello.Abstractions.Factories
{
    public interface ITarefaFactory
    {
        Tarefa Criar(Card card);
        void AtualizarFeatures(IEnumerable<Feature> features);
    }
}