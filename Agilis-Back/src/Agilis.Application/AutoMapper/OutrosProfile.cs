using AutoMapper;
using Agilis.Application.ViewModels.Mensagens;
using Agilis.Core.Domain.Models.Entities;
using Agilis.Core.Domain.Models.ValueObjects;

namespace Agilis.Application.AutoMapper
{
    public class OutrosProfile : Profile
    {
        public OutrosProfile()
        {
            CreateMap<string, Email>()
              .ConstructUsing(email => new Email(email));

            CreateMap<Email, string>()
              .ConstructUsing(e => e.Endereco);

            CreateMap<EmailViewModel, Email>()
              .ConstructUsing(vm => new Email(vm.Endereco));

            CreateMap<string, HtmlColor>()
              .ConstructUsing(cor => new HtmlColor(cor));

            CreateMap<HtmlColor, string>()
              .ConstructUsing(hc => hc.Codigo);

            CreateMap<int, DiaDoMes>()
              .ConstructUsing(dia => new DiaDoMes(dia));

            CreateMap<DiaDoMes, int>()
              .ConstructUsing(dm => dm.Dia);

            CreateMap<Dispositivo, DispositivoViewModel>();

            CreateMap<DispositivoViewModel, Dispositivo>()
              .ConstructUsing((vm, context) => new Dispositivo(vm.Token));
        }
    }
}
