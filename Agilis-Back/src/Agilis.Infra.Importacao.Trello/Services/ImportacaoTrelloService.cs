using Agilis.Core.Domain.Abstractions.Repositories;
using Agilis.Core.Domain.Abstractions.UnitsOfWork;
using Agilis.Core.Domain.Models.Entities;
using Agilis.Core.Domain.Models.Entities.Seguranca;
using Agilis.Core.Domain.Models.Entities.Tarefas;
using Agilis.Infra.Importacao.Trello.Abstractions.Services;
using Agilis.Infra.Importacao.Trello.Extensions;
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
        private readonly List<string> _tags = new List<string>();
        private readonly List<Usuario> _usuarios = new List<Usuario>();

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
            await ImportarTrelloAsync();

            await SalvarDados();

            _logger.LogInformation(new String('-', 50));
            _logger.LogInformation("Importação concluída com sucesso");
            _logger.LogInformation(new String('-', 50));
        }

        private async Task SalvarDados()
        {
            _logger.LogInformation(new String('-', 50));
            _logger.LogInformation("Salvando os dados...");
            await _unitOfWork.CommitAsync();
        }

        private async Task LimparDadosAsync()
        {
            _logger.LogInformation(new String('-', 50));
            _logger.LogInformation("Limpando o banco de dados...");

            var tarefaRepository = _unitOfWork.ObterRepository<Tarefa>();
            var usuarioRepository = _unitOfWork.ObterRepository<Usuario>();
            var sprintRepository = _unitOfWork.ObterRepository<Sprint>();

            await tarefaRepository.ExcluirAsync(t => true);
            await sprintRepository.ExcluirAsync(s => true);
            await usuarioRepository.ExcluirAsync(u => true);

            _usuarios.Clear();
            _tags.Clear();
        }

        private async Task ImportarTrelloAsync()
        {
            //TODO: configurar organization id
            const string ORGANIZATION_ID = "erp2113";
            _logger.LogInformation("Obtendo dados do Trello...");
            //var boards = _trelloEasyService.GetBoards(ORGANIZATION_ID, BoardFilter.All);

            var backlogRede = _trelloEasyService.GetBoard("6203a3dca53d7503af47e6ba");
            var boards = new List<Board> { backlogRede };

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
            var anexoRepository = _unitOfWork.ObterRepository<Anexo>();

            _logger.LogInformation($"Importanto cards...");

            foreach (var list in board.Lists)
            {
                foreach (var card in list.Cards)
                {
                    _logger.LogInformation(new String('-', 50));
                    _logger.LogInformation($"Importando card {card.Name} - {card.ShortUrl}...");

                    await ImportarUsuariosAsync(card);
                    ImportarTags(card);
                    await ImportarAnexos(anexoRepository, card);

                    var tarefa = _mapper.Map<Tarefa>(card);
                    if (tarefa.Invalido)
                        _logger.LogError("Tarefa inválida");
                    else
                        await tarefaRepository.AdicionarAsync(tarefa);
                }
            }
        }

        private async Task ImportarAnexos(IRepository<Anexo> anexoRepository, Card card)
        {
            foreach (var attachment in card.Attachments)
            {
                _logger.LogInformation($"Importando anexo {attachment.Name}...");

                var anexo = _mapper.Map<Anexo>(attachment);

                await anexoRepository.AdicionarAsync(anexo);

                attachment.AtualizarId(anexo.Id.ToString());
            }
        }

        private void ImportarTags(Card card)
        {
            foreach (var label in card.Labels)
            {
                _logger.LogInformation($"Importando #{label.Name}...");
                if (!_tags.Contains(label.Name))
                    _tags.Add(label.Name);
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
            var usuarioRepository = _unitOfWork.ObterRepository<Usuario>();

            var membros = card.Members.Concat(card.Actions.Select(a => a.MemberCreator));

            foreach (var membro in membros)
            {
                var usuarioJaCadastrado = _usuarios.FirstOrDefault(u => u.NomeCompleto == membro.ObterNomeCompleto());
                if (usuarioJaCadastrado != null)
                {
                    membro.AtualizarId(usuarioJaCadastrado.Id.ToString());
                }
                else
                {
                    _logger.LogInformation($"Importando @{membro.UserName}...");
                    var novoUsuario = _mapper.Map<Usuario>(membro);

                    if (novoUsuario.Invalido)
                        _logger.LogError("Usuário inválido");
                    else
                    {
                        await usuarioRepository.AdicionarAsync(novoUsuario);
                        membro.AtualizarId(novoUsuario.Id.ToString());
                        _usuarios.Add(novoUsuario);
                    }
                }
            }
        }


    }
}
