using Agilis.Core.Domain.Models.Entities.Seguranca;
using Agilis.Core.Domain.Models.ValueObjects.Tarefas;
using AutoMapper;
using Action = TrelloSharpEasy.Entities.Action;

namespace Agilis.Infra.Importacao.Trello.AutoMapper
{
    public class ActionProfile : Profile
    {
        public ActionProfile()
        {
            CreateMap<Action, Comentario>()
               .ConvertUsing((action, x, context) =>
                   new Comentario(
                       action.Text,
                       context.Mapper.Map<Usuario>(action.MemberCreator),
                       action.Date
                       )
                   );
        }
    }
}
