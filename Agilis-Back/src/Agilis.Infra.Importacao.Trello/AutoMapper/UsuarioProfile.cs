using Agilis.Core.Domain.Enums;
using Agilis.Core.Domain.Models.Entities.Seguranca;
using Agilis.Core.Domain.Models.ValueObjects;
using Agilis.Core.Domain.Models.ValueObjects.Seguranca;
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
                    new Usuario(
                        nome: ObterNome(membro),
                        sobrenome: ObterSobrenome(membro),
                        senha: new Senha("123"),
                        email: new Email($"{membro.UserName}@trello.com"),
                        ativo: true,
                        regra: RegraUsuario.Usuario
                        )
                    );
        }

        private static string ObterSobrenome(Member membro)
        {
            if (!membro.FullName.Contains(" "))
                return String.Empty;

            var nome = ObterNome(membro);
            return membro.FullName.Substring(nome.Length + 1);
        }

        private static string ObterNome(Member membro)
        {
            return membro.FullName.Split(' ')[0];
        }
    }
}
