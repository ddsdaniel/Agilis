using AutoMapper;
using Agilis.Application.ViewModels.Mensagens;
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

            CreateMap<Hora, string>()
                .ConstructUsing(hora => hora.Horario);

            CreateMap<string, Hora>()
                .ConstructUsing(horario => new Hora(horario));

            CreateMap<string, Tag>()
             .ConstructUsing(nome => new Tag(nome));

            CreateMap<Tag, string>()
              .ConstructUsing(t => t.Nome);
        }
    }
}
