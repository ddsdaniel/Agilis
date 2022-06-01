using Agilis.Infra.Importacao.Trello.ViewModels;
using DDS.Validacoes.Abstractions.Models;

namespace Agilis.Infra.Importacao.Trello.Abstractions.Services
{
    public interface IImportacaoTrelloService : IValidavel
    {
        Task ImportarAsync(ImportacaoViewModel importacaoViewModel);
    }
}