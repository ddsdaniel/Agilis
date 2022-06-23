using Agilis.Core.Domain.Models.ValueObjects.Tarefas;
using AutoMapper;
using TrelloSharpEasy.Entities;
using CheckListAgilis = Agilis.Core.Domain.Models.ValueObjects.Tarefas.CheckList;
using CheckListTrello = TrelloSharpEasy.Entities.CheckList;

namespace Agilis.Infra.Importacao.Trello.AutoMapper
{
    public class CheckListProfile : Profile
    {
        public CheckListProfile()
        {
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
        }
    }
}
