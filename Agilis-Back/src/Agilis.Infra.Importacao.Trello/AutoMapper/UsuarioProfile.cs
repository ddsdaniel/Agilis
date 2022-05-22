using Agilis.Core.Domain.Enums;
using Agilis.Core.Domain.Models.Entities.Seguranca;
using Agilis.Core.Domain.Models.ValueObjects;
using Agilis.Core.Domain.Models.ValueObjects.Seguranca;
using Agilis.Infra.Importacao.Trello.Extensions;
using AutoMapper;
using TrelloSharpEasy.Entities;

namespace Agilis.Infra.Importacao.Trello.AutoMapper
{
    public class UsuarioProfile : Profile
    {
        public UsuarioProfile()
        {
            CreateMap<Member, Usuario>()
                .ConvertUsing((membro, context) =>
                {
                    var email = membro.FullName == "Daniel Dorneles da Silva"
                        ? "dds.daniel@gmail.com"
                        : $"{membro.UserName}@trello.com";

                    var usuario = new Usuario(
                        nome: ObterNome(membro),
                        sobrenome: ObterSobrenome(membro),
                        senha: new Senha("123"),
                        email: new Email(email),
                        ativo: true,
                        regra: RegraUsuario.Usuario
                        );

                    if (membro.Id.Contains('-'))
                        usuario.AtualizarId(new Guid(membro.Id));

                    return usuario;
                });
        }

        private static string ObterSobrenome(Member membro)
        {
            var nome = ObterNome(membro);
            return membro.FullName.Substring(nome.Length + 1);
        }

        private static string ObterNome(Member membro)
        {
            return membro.ObterNomeCompleto().Split(' ')[0];
        }
    }
}
