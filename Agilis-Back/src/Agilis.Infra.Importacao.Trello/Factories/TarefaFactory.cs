using Agilis.Core.Domain.Abstractions.Repositories;
using Agilis.Core.Domain.Abstractions.UnitsOfWork;
using Agilis.Core.Domain.Enums;
using Agilis.Core.Domain.Models.Entities;
using Agilis.Core.Domain.Models.Entities.Seguranca;
using Agilis.Core.Domain.Models.Entities.Tarefas;
using Agilis.Core.Domain.Models.ValueObjects;
using Agilis.Core.Domain.Models.ValueObjects.Tarefas;
using Agilis.Infra.Importacao.Trello.Abstractions.Factories;
using AutoMapper;
using System.Text.RegularExpressions;
using TrelloSharpEasy.Entities;
using CheckListAgilis = Agilis.Core.Domain.Models.ValueObjects.Tarefas.CheckList;

namespace Agilis.Infra.Importacao.Trello.Factories
{
    public class TarefaFactory : ITarefaFactory
    {
        private readonly IMapper _mapper;
        private readonly List<Cliente> _clientes;
        public IEnumerable<Feature> _features;

        public TarefaFactory(
            IMapper mapper,
            IUnitOfWork unitOfWork
            )
        {
            _mapper = mapper;

            var clienteRepository = unitOfWork.ObterRepository<Cliente>();
            _clientes = clienteRepository.Consultar().ToList();
        }

        public void AtualizarFeatures(IEnumerable<Feature> features)
        {
            _features = features;
        }

        public Tarefa Criar(Card card)
        {
            return new Tarefa(
                   titulo: ObterTitulo(card),
                   descricao: card.Description,
                   feature: ObterFeature(card),
                   tipo: ObterTipo(card),
                   relator: ObterRelator(card),
                   solucionador: _mapper.Map<Usuario>(card.Members.FirstOrDefault()),
                   horasPrevistas: ObterHoras(card),
                   horasRealizadas: ObterHoras(card),
                   tags: card.Labels.Select(label => new Tag(label.Name)),
                   checkLists: _mapper.Map<CheckListAgilis[]>(card.CheckLists),
                   cliente: ObterCliente(card),
                   valor: 0,
                   urlTicketSAC: null,
                   comentarios: _mapper.Map<Comentario[]>(card.Actions.Where(a => a.Type == "commentCard")),
                   anexos: _mapper.Map<AnexoFK[]>(card.Attachments),
                   sprint: ObterSprint(card),
                   situacao: ObterSituacao(card),
                   solucao: ObterSolucao(card),
                   branches: ObterBranches(card)
               );
        }

        private Usuario ObterRelator(Card card)
        {
            var relator = _mapper.Map<Usuario>(card.Actions.OrderBy(a => a.Date).FirstOrDefault()?.MemberCreator);
            if (relator == null)
                return null;

            return relator;
        }

        private string ObterTitulo(Card card)
        {
            var titulo = card.Name;
            var horas = ObterHoras(card);
            if (horas is not null)
                titulo = titulo.Replace($"({horas.Horario})", "").Trim();

            return titulo;
        }

        private Sprint ObterSprint(Card card)
        {
            if (card.BoardName.StartsWith("Sprint"))
                return new Sprint(card.BoardName, null, null, string.Empty);

            return null;
        }

        private string ObterBranches(Card card)
        {
            var actionBranch = card.Actions
                .OrderBy(a => a.Date)
                .LastOrDefault(a => a.Text != null && a.Text.StartsWith("# Branch"));

            if (actionBranch != null)
            {
                return actionBranch.Text;
            }
            else
                return String.Empty;
        }

        private string ObterSolucao(Card card)
        {
            if (!card.Actions.Any())
                return ".";

            var actionSolucao = card.Actions
                .OrderBy(a => a.Date)
                .LastOrDefault(a => a.Text != null && a.Text.StartsWith("# Solução"));

            if (actionSolucao != null)
            {
                return actionSolucao.Text;
            }
            else
                return ".";
        }

        private Cliente ObterCliente(Card card)
        {
            var cliente = _clientes.FirstOrDefault(c => ContemLabel(card, c.Nome));
            return cliente;
        }

        private Feature ObterFeature(Card card)
        {
            return _features.FirstOrDefault(f => f.Nome.ToUpper() == card.ListName.ToUpper());
        }

        private SituacaoTarefa ObterSituacao(Card card)
        {
            if (ObterSprint(card) == null)
                return SituacaoTarefa.AFazer;

            switch (card.ListName)
            {
                case "Caixa de Entrada":
                case "Não Analisado":
                    return SituacaoTarefa.AFazer;
                case "Analisando":
                case "Não Programado":
                case "Programando":
                case "Não Testado":
                case "Testando":
                case "Aguardando Dependências":
                    return SituacaoTarefa.Fazendo;
                default:
                    return SituacaoTarefa.Feito;
            }
        }

        private Hora ObterHoras(Card card)
        {
            const string PATTERN = @"\([0-9]?[0-9]:[0-5][0-9]\)";
            var regex = new Regex(PATTERN);
            if (regex.IsMatch(card.Name))
            {
                var horas = regex
                    .Match(card.Name)
                    .Value
                    .Replace("(", "")
                    .Replace(")", "");

                return new Hora(horas);
            }
            return null;
        }

        private TipoTarefa ObterTipo(Card card)
        {
            if (ContemLabel(card, "hotfix") || ContemLabel(card, "bugfix") || ContemLabel(card, "bug"))
                return TipoTarefa.Bug;

            if (ContemLabel(card, "feature"))
                return TipoTarefa.Novidade;

            return TipoTarefa.NaoIdentificado;
        }

        private bool ContemLabel(Card card, string labelProcurada)
        {
            return card.Labels.Any(label => label.Name.ToLower() == labelProcurada.ToLower());
        }
    }
}
