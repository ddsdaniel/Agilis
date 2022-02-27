using Flunt.Validations;
using Agilis.Core.Domain.Abstractions.Models.ValueObjects;
using Agilis.Core.Domain.Extensions;

namespace Agilis.Infra.Seguranca.Models.ValueObjects
{
    //TODO: mover para o projeto de segurança
    public class AlterarMinhaSenha : ValueObject<AlterarMinhaSenha>
    {
        public Senha SenhaAtual { get; private set; }
        public Senha NovaSenha { get; private set; }
        public Senha ConfirmaSenha { get; private set; }

        protected AlterarMinhaSenha() { }

        public AlterarMinhaSenha(Senha senhaAtual, Senha novaSenha, Senha confirmaSenha)
        {
            SenhaAtual = senhaAtual;
            NovaSenha = novaSenha;
            ConfirmaSenha = confirmaSenha;

            AddNotifications(new Contract()
                .IsValid(SenhaAtual, nameof(SenhaAtual))
                .IsValid(NovaSenha, nameof(NovaSenha))
                .IsValid(ConfirmaSenha, nameof(ConfirmaSenha))
                );

            if (NovaSenha?.Conteudo != null && ConfirmaSenha?.Conteudo != null)
            {
                if (NovaSenha.Conteudo != ConfirmaSenha.Conteudo)
                {
                    AddNotification(nameof(ConfirmaSenha), "Nova Senha e Confirma Senha não devem ser diferentes");
                }

                if (SenhaAtual?.Conteudo != null)
                {
                    if (SenhaAtual.Conteudo == NovaSenha.Conteudo)
                        AddNotification(nameof(NovaSenha), "A nova senha não deve ser igual à senha atual");
                }
            }
        }
    }
}
