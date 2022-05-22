using Agilis.Core.Domain.Enums;
using Agilis.Core.Domain.Models.Entities;
using Agilis.Core.Domain.Models.Entities.Seguranca;
using Agilis.Core.Domain.Models.Entities.Tarefas;
using Agilis.Core.Domain.Models.ValueObjects;
using Agilis.Core.Domain.Models.ValueObjects.Tarefas;
using AutoMapper;
using System.Text.RegularExpressions;
using TrelloSharpEasy.Entities;
using Action = TrelloSharpEasy.Entities.Action;
using CheckListAgilis = Agilis.Core.Domain.Models.ValueObjects.Tarefas.CheckList;
using CheckListTrello = TrelloSharpEasy.Entities.CheckList;

namespace Agilis.Infra.Importacao.Trello.AutoMapper
{
    public class TarefaProfile : Profile
    {
        public static List<Cliente> Clientes { get; internal set; }

        public TarefaProfile()
        {
            CreateMap<Card, Tarefa>()
                .ConvertUsing((card, x, context) =>
                new Tarefa(
                    titulo: ObterTitulo(card),
                    descricao: card.Description,
                    feature: ObterFeature(card),
                    tipo: ObterTipo(card),
                    relator: ObterRelator(card, context),
                    solucionador: context.Mapper.Map<Usuario>(card.Members.FirstOrDefault()),
                    horasPrevistas: ObterHoras(card),
                    horasRealizadas: ObterHoras(card),
                    tags: card.Labels.Select(label => new Tag(label.Name)),
                    checkLists: context.Mapper.Map<CheckListAgilis[]>(card.CheckLists),
                    cliente: ObterCliente(card),
                    valor: 0,
                    urlTicketSAC: null,
                    comentarios: context.Mapper.Map<Comentario[]>(card.Actions.Where(a => a.Type == "commentCard")),
                    anexos: context.Mapper.Map<AnexoFK[]>(card.Attachments),
                    sprint: ObterSprint(card),
                    situacao: ObterSituacao(card),
                    solucao: ObterSolucao(card),
                    branches: ObterBranches(card)
                    )
                );

            CreateMap<CheckListTrello, CheckListAgilis>()
                .ConvertUsing((checklist, x, context) =>
                    new CheckListAgilis(
                        nome: checklist.Name,
                        itens: context.Mapper.Map<ItemCheckList[]>(checklist.Items)
                        )
                    );

            CreateMap<CheckItem, ItemCheckList>()
                .ConvertUsing((item, x, context) =>
                    new ItemCheckList(
                        nome: item.Name,
                        concluido: item.Checked,
                        horasPrevistas: null
                        )
                    );

            CreateMap<Action, Comentario>()
                .ConvertUsing((action, x, context) =>
                    new Comentario(
                        action.Text,
                        context.Mapper.Map<Usuario>(action.MemberCreator),
                        action.Date
                        )
                    );

            CreateMap<Attachment, Anexo>()
               .ConvertUsing((attachment, x, context) =>
                   new Anexo(
                       nome: attachment.Name,
                       conteudo: attachment.Url
                       )
                   );

            CreateMap<Attachment, AnexoFK>()
              .ConvertUsing((attachment, x, context) =>
                  new AnexoFK(
                      nome: attachment.Name,
                      anexoId: new Guid(attachment.Id)
                      )
                  );

        }

        private static Usuario ObterRelator(Card card, ResolutionContext context)
        {
            var relator = context.Mapper.Map<Usuario>(card.Actions.OrderBy(a => a.Date).FirstOrDefault()?.MemberCreator);
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
            var cliente = Clientes.FirstOrDefault(c => ContemLabel(card, c.Nome));
            return cliente;
        }

        private Feature ObterFeature(Card card)
        {
            //TODO: label que não é cliente e nem branch
            return null;
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
