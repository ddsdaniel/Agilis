using DDS.Domain.Core.Abstractions.Services;
using DDS.Domain.Core.Model.ValueObjects;
using DDS.Domain.Core.Model.ValueObjects.Seguranca.Senhas;
using System;
using System.Threading.Tasks;
using Agilis.Domain.Models.Entities.Pessoas;
using Agilis.Domain.Models.ValueObjects.Seguranca;

namespace Agilis.Domain.Abstractions.Services.Seguranca
{
    public interface IUsuarioService : ICrudService<Usuario>
    {
        public Usuario Autenticar(Login login);
        public Task AlterarSenha(Guid id, Email emailLogado, SenhaMedia senhaAtual, SenhaMedia novaSenha, SenhaMedia confirmaNovaSenha);
    }
}
