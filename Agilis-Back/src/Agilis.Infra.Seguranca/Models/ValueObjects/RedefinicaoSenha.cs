using Agilis.Core.Domain.Abstractions.Models.ValueObjects;

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
            Validar();
        }

        private void Validar()
        {
            ImportarCriticas(NovaSenha);
            ImportarCriticas(ConfirmaSenha);

            if (NovaSenha?.Conteudo != null && ConfirmaSenha?.Conteudo != null)
            {
                if (NovaSenha?.Conteudo != ConfirmaSenha?.Conteudo)
                    Criticar("Nova Senha e Confirma Senha não devem ser diferentes");
            }
        }
    }
}
