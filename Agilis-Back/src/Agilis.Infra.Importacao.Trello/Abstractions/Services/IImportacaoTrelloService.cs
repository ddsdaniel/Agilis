using DDS.Validacoes.Abstractions.Models;

namespace Agilis.Infra.Importacao.Trello.Abstractions.Services
{
    public interface IImportacaoTrelloService : IValidavel
    {
        Task ImportarAsync();
    }
}