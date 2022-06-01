using Agilis.Application.Services.Tarefas;
using Agilis.Core.Domain.Abstractions.Repositories;
using Agilis.Core.Domain.Abstractions.UnitsOfWork;
using Agilis.Core.Domain.Models.Entities;
using Agilis.Core.Domain.Models.Entities.Seguranca;
using Agilis.Core.Domain.Models.Entities.Tarefas;
using Agilis.Infra.Importacao.Trello.Abstractions.Factories;
using Agilis.Infra.Importacao.Trello.Abstractions.Services;
using Agilis.Infra.Importacao.Trello.Extensions;
using Agilis.Infra.Importacao.Trello.ViewModels;
using AutoMapper;
using DDS.Validacoes.Abstractions.Models;
using Microsoft.Extensions.Logging;
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
        private readonly TarefaCrudAppService _tarefaCrudAppService;
        private readonly ITarefaFactory _tarefaFactory;
        private readonly IFeatureFactory _featureFactory;
        private List<string> _tags = new List<string>();
        private List<Usuario> _usuarios = new List<Usuario>();

        public ImportacaoTrelloService(
            IMapper mapper,
            IUnitOfWork unitOfWork,
            ILogger<ImportacaoTrelloService> logger,
            EasyService easyService,
            TarefaCrudAppService tarefaCrudAppService,
            ITarefaFactory tarefaFactory,
            IFeatureFactory featureFactory)
        {
            _featureFactory = featureFactory;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _logger = logger;
            _trelloEasyService = easyService;
            _tarefaCrudAppService = tarefaCrudAppService;
            _tarefaFactory = tarefaFactory;
        }

        public async Task ImportarAsync(ImportacaoViewModel importacaoViewModel)
        {
            await ImportarTrelloAsync(importacaoViewModel);

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
            var clienteRepository = _unitOfWork.ObterRepository<Cliente>();
            var featureRepository = _unitOfWork.ObterRepository<Feature>();

            await tarefaRepository.ExcluirAsync(t => true);
            await sprintRepository.ExcluirAsync(s => true);
            await usuarioRepository.ExcluirAsync(u => true);
        }

        private void InicializarCaches(ImportacaoViewModel importacaoViewModel)
        {
            var usuarioRepository = _unitOfWork.ObterRepository<Usuario>();

            //TarefaProfile.Features = featureRepository.Consultar().ToList();
            //TarefaProfile.Clientes = clienteRepository.Consultar().ToList();
            //TarefaProfile.Produto = _mapper.Map<Produto>(importacaoViewModel.Produto);
            _usuarios = usuarioRepository.Consultar().ToList();
            _tags = _tarefaCrudAppService.ConsultarTags().ToList();
        }

        private async Task ImportarTrelloAsync(ImportacaoViewModel importacaoViewModel)
        {
            _logger.LogInformation("Obtendo dados do Trello...");

            var backlog = _trelloEasyService.GetBoard(importacaoViewModel.BoardId);

            var boards = new List<Board> { backlog };

            if (importacaoViewModel.LimparDados)
                await LimparDadosAsync();

            InicializarCaches(importacaoViewModel);

            var produto = _mapper.Map<Produto>(importacaoViewModel.Produto);

            foreach (var board in boards)
            {
                await ImportarFeaturesAsync(board, produto);
                await ImportarSprintAsync(board);
                await ImportarTarefasAsync(board);
            }
        }

        private async Task ImportarFeaturesAsync(Board board, Produto produto)
        {
            var featureRepository = _unitOfWork.ObterRepository<Feature>();
            var features = featureRepository.Consultar().ToList();
            var achou = false;

            foreach (var list in board.Lists)
            {
                var feature = _featureFactory.Criar(list, produto);
                if (!features.Any(f => f.Nome == feature.Nome && f.Produto.Id == produto.Id))
                {
                    await featureRepository.AdicionarAsync(feature);
                    features.Add(feature);
                    achou = true;
                }
            }

            if (achou)
                _tarefaFactory.AtualizarFeatures(features);
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
