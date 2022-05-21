using Agilis.Core.Domain.Abstractions.UnitsOfWork;
using Agilis.Core.Domain.Models.Entities;
using Agilis.Core.Domain.Models.Entities.Seguranca;
using Agilis.Core.Domain.Models.Entities.Tarefas;
using Agilis.Infra.Importacao.Trello.Abstractions.Services;
using AutoMapper;
using DDS.Validacoes.Abstractions.Models;
using Microsoft.Extensions.Logging;
using TrelloSharp.Enums;
using TrelloSharpEasy.Entities;
using TrelloSharpEasy.Services;

namespace Agilis.Infra.Importacao.Trello.Services
{
    public class ImportacaoTrelloService : Validavel, IImportacaoTrelloService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<ImportacaoTrelloService> _logger;
        private readonly EasyService _trelloEasyService;
        private readonly List<string> Tags = new List<string>();

        public ImportacaoTrelloService(
            IMapper mapper,
            IUnitOfWork unitOfWork,
            ILogger<ImportacaoTrelloService> logger,
            EasyService easyService
            )
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _logger = logger;
            _trelloEasyService = easyService;
        }

        public async Task ImportarAsync()
        {            
            await ImportarSprintsAsync();

            await SalvarDados();

            _logger.LogInformation("Importação concluída com sucesso");
        }

        private async Task SalvarDados()
        {
            _logger.LogInformation("Salvando os dados...");
            await _unitOfWork.CommitAsync();
        }

        private async Task LimparDadosAsync()
        {
            var tarefaRepository = _unitOfWork.ObterRepository<Tarefa>();
            var usuarioRepository = _unitOfWork.ObterRepository<Usuario>();
            var sprintRepository = _unitOfWork.ObterRepository<Sprint>();

            await tarefaRepository.ExcluirAsync(t => true);
            await sprintRepository.ExcluirAsync(s => true);
            await usuarioRepository.ExcluirAsync(u => true);

            Tags.Clear();
        }

        private async Task ImportarSprintsAsync()
        {
            //TODO: configurar organization id
            const string ORGANIZATION_ID = "erp2113";
            _logger.LogInformation("Obtendo quadros...");
            var boards = _trelloEasyService.GetBoards(ORGANIZATION_ID, BoardFilter.All);

            await LimparDadosAsync();

            foreach (var board in boards)
            {
                await ImportarSprintAsync(board);                
                await ImportarTarefasAsync(board);
            }
        }

        private async Task ImportarTarefasAsync(Board board)
        {
            var tarefaRepository = _unitOfWork.ObterRepository<Tarefa>();

            _logger.LogInformation($"Importanto cards...");

            foreach (var list in board.Lists)
            {
                foreach (var card in list.Cards)
                {
                    await ImportarUsuariosAsync(card);
                    ImportarTags(card);

                    _logger.LogInformation($"Importando card {card.ShortUrl}...");

                    var tarefa = _mapper.Map<Tarefa>(card);
                    if (tarefa.Invalido)
                        _logger.LogError("Tarefa inválida");
                    else
                        await tarefaRepository.AdicionarAsync(tarefa);
                }
            }
        }

        private void ImportarTags(Card card)
        {
            _logger.LogInformation("Obtendo tags...");
            foreach (var label in card.Labels)
            {
                _logger.LogInformation($"Importando {label.Name}...");
                if (!Tags.Contains(label.Name))
                    Tags.Add(label.Name);
            }
        }

        private async Task ImportarSprintAsync(Board board)
        {
            var sprintRepository = _unitOfWork.ObterRepository<Sprint>();

            _logger.LogInformation($"Importanto quadro {board.Name}...");
            if (board.Name.StartsWith("Sprint"))
            {
                var sprint = _mapper.Map<Sprint>(board);
                if (sprint.Invalido)
                    _logger.LogError("Sprint inválido");
                else
                    await sprintRepository.AdicionarAsync(sprint);
            }
        }

        private async Task ImportarUsuariosAsync(Card card)
        {
            _logger.LogInformation("Obtendo membros...");

            var usuarioRepository = _unitOfWork.ObterRepository<Usuario>();

            foreach (var member in card.Members)
            {
                _logger.LogInformation($"Importando {member.FullName}...");
                var usuario = _mapper.Map<Usuario>(member);

                if (usuario.Invalido)
                    _logger.LogError("Usuário inválido");
                else
                    await usuarioRepository.AdicionarAsync(usuario);
            }
        }
    }
}
