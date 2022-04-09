using Agilis.Core.Domain.Abstractions.Models.ValueObjects;

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
            Validar();
        }

        private void Validar()
        {
            ImportarCriticas(SenhaAtual);
            ImportarCriticas(NovaSenha);
            ImportarCriticas(ConfirmaSenha);

            if (NovaSenha?.Conteudo != null && ConfirmaSenha?.Conteudo != null)
            {
                if (NovaSenha.Conteudo != ConfirmaSenha.Conteudo)
                {
                    Criticar("Nova Senha e Confirma Senha não devem ser diferentes");
                }

                if (SenhaAtual?.Conteudo != null)
                {
                    if (SenhaAtual.Conteudo == NovaSenha.Conteudo)
                        Criticar("A nova senha não deve ser igual à senha atual");
                }
            }
        }
    }
}
