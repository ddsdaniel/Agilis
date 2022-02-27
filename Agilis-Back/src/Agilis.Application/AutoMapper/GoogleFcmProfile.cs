using AutoMapper;
using Agilis.Core.Domain.Models.ValueObjects;
using Agilis.Infra.Notifications.FirebaseCloudMessaging.Models.ValueObjects;
using System.Linq;

namespace Agilis.Application.AutoMapper
{
    public class GoogleFcmProfile : Profile
    {
        public GoogleFcmProfile()
        {
            CreateMap<Notificacao, GoogleFcmMessage>()
                .ConstructUsing((notificacao, context) =>
                    new GoogleFcmMessage
                    {
                        Notification = new GoogleFcmNotification
                        {
                            Title = notificacao.Titulo,
                            Body = notificacao.Corpo,
                            Icon = notificacao.Icone,
                            Click_action = notificacao.ClickAction
                        },
                        Registration_ids = notificacao.Dispositivos.Select(d => d.Token).ToArray()
                    }
                );
        }
    }
}
