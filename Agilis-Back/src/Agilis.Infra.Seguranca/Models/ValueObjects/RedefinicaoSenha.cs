using Flunt.Validations;
using Agilis.Core.Domain.Abstractions.Models.ValueObjects;
using Agilis.Core.Domain.Extensions;

namespace Agilis.Infra.Seguranca.Models.ValueObjects
{
    public class RedefinicaoSenha : ValueObject<RedefinicaoSenha>
    {
        public Senha NovaSenha { get; private set; }
        public Senha ConfirmaSenha { get; private set; }

        protected RedefinicaoSenha() { }

        public RedefinicaoSenha(Senha novaSenha, Senha confirmaSenha)
        {
            NovaSenha = novaSenha;
            ConfirmaSenha = confirmaSenha;

            AddNotifications(new Contract()
                .IsValid(NovaSenha, nameof(NovaSenha))
                .IsValid(ConfirmaSenha, nameof(ConfirmaSenha))
                );

            if (NovaSenha?.Conteudo != null && ConfirmaSenha?.Conteudo != null)
            {
                if (NovaSenha?.Conteudo != ConfirmaSenha?.Conteudo)
                    AddNotification(nameof(ConfirmaSenha), "Nova Senha e Confirma Senha não devem ser diferentes");
            }
        }
    }
}
