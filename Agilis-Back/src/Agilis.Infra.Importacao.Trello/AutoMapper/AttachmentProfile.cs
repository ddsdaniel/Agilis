using Agilis.Core.Domain.Models.Entities;
using Agilis.Core.Domain.Models.ValueObjects;
using AutoMapper;
using TrelloSharpEasy.Entities;

namespace Agilis.Infra.Importacao.Trello.AutoMapper
{
    public class AttachmentProfile : Profile
    {
        public AttachmentProfile()
        {
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
    }
}
