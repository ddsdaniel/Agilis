using Agilis.Core.Domain.Abstractions.Models.Entities;
using Agilis.Core.Domain.Enums;
using Agilis.Core.Domain.Models.ValueObjects;
using Agilis.Core.Domain.Models.ValueObjects.Seguranca;

namespace Agilis.Core.Domain.Models.Entities.Seguranca
{
    public class Usuario : Entidade, IUsuario
    {
        public string Nome { get; private set; }
        public string Sobrenome { get; private set; }
        public string NomeCompleto => $"{Nome} {Sobrenome}";
        public Senha Senha { get; private set; }
        public Email Email { get; private set; }
        public bool Ativo { get; private set; }
        public RegraUsuario Regra { get; private set; }

        protected Usuario() { }

        public Usuario(string nome,
                       string sobrenome,
                       Senha senha,
                       Email email,
                       bool ativo,
                       RegraUsuario regra)
        {
            Nome = nome;
            Sobrenome = sobrenome;
            Senha = senha;
            Email = email;
            Ativo = ativo;
            Regra = regra;

            Validar();
        }

        private void Validar()
        {
            if (string.IsNullOrEmpty(Nome))
                Criticar("Nome não deve ser nulo ou vazio");

            if (string.IsNullOrEmpty(Sobrenome))
                Criticar("Sobrenome não deve ser nulo ou vazio");

            ImportarCriticas(Email);
            ImportarCriticas(Senha);
        }

        public void AlterarSenha(AlterarMinhaSenha alterarMinhaSenha)
        {
            if (alterarMinhaSenha == null)
            {
                Criticar("O objeto alterar a minha senha não deve ser nulo");
                return;
            }
            ImportarCriticas(alterarMinhaSenha);

            if (Invalido) return;

            if (alterarMinhaSenha.SenhaAtual.Conteudo != Senha.Conteudo)
            {
                Criticar("A senha atual está incorreta");
                return;
            }

            Senha = alterarMinhaSenha.NovaSenha;
        }

        public override string ToString()
        {
            return NomeCompleto;
        }

        public void RedefinirSenha(RedefinicaoSenha redefinicaoSenha)
        {
            if (redefinicaoSenha == null)
            {
                Criticar("O objeto redefinição de minha senha não deve ser nulo");
                return;
            }
            ImportarCriticas(redefinicaoSenha);
            if (Invalido) return;

            Senha = redefinicaoSenha.NovaSenha;
        }
    }
}
