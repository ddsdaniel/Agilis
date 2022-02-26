using DDS.Domain.Core.Abstractions.Services;
using DDS.Domain.Core.Models.ValueObjects;
using DDS.Domain.Core.Models.ValueObjects.Seguranca.Senhas;
using System;
using System.Threading.Tasks;
using Agilis.Domain.Models.Entities.Pessoas;
using Agilis.Domain.Models.ValueObjects.Seguranca;

namespace Agilis.Domain.Abstractions.Services.Pessoas
{
    public interface IUsuarioService : ICrudService<Usuario>
    {
        public Usuario Autenticar(Login login);
        public Task AlterarSenha(Guid id, Email emailLogado, SenhaMedia senhaAtual, SenhaMedia novaSenha, SenhaMedia confirmaNovaSenha);
        public Usuario ConsultarPorEmail(Email email);
    }
}
