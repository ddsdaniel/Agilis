using Flunt.Validations;
using Agilis.Core.Domain.Abstractions.Models.Entities;
using Agilis.Core.Domain.Enums;
using Agilis.Core.Domain.Extensions;
using Agilis.Core.Domain.Models.ValueObjects;
using Agilis.Infra.Seguranca.Abstractions.Models.Entities;
using Agilis.Infra.Seguranca.Models.ValueObjects;

namespace Agilis.Infra.Seguranca.Models.Entities
{
    public class Usuario : Entidade, IUsuario
    {
        public string Nome { get; private set; }
        public string Sobrenome { get; private set; }
        public string NomeCompleto => $"{Nome} {Sobrenome}";
        public Senha Senha { get; private set; }
        public Email Email { get; private set; }
        public bool Ativo { get; private set; }
        public bool LicencaCompleta { get; private set; }
        public RegraUsuario Regra { get; private set; }

        protected Usuario() { }

        public Usuario(string nome,
                       string sobrenome,
                       Senha senha,
                       Email email,
                       bool ativo,
                       bool licencaCompleta,
                       RegraUsuario regra)
        {
            Nome = nome;
            Sobrenome = sobrenome;
            Senha = senha;
            Email = email;
            Ativo = ativo;
            LicencaCompleta = licencaCompleta;
            Regra = regra;

            AddNotifications(new Contract()
                .IsNotNullOrEmpty(Nome, nameof(Nome), "Nome não deve ser nulo ou vazio")
                .IsNotNullOrEmpty(Sobrenome, nameof(Sobrenome), "Sobrenome não deve ser nulo ou vazio")
                .IsValid(Email, nameof(Email))
                .IsValid(Senha, nameof(Senha))
                );
        }

        public void AlterarSenha(AlterarMinhaSenha alterarMinhaSenha)
        {
            if (alterarMinhaSenha == null)
            {
                AddNotification(nameof(alterarMinhaSenha), "O objeto alterar a minha senha não deve ser nulo");
                return;
            }
            AddNotifications(alterarMinhaSenha);
            if (Invalid) return;

            if (alterarMinhaSenha.SenhaAtual.Conteudo != Senha.Conteudo)
            {
                AddNotification(nameof(alterarMinhaSenha.SenhaAtual), "A senha atual está incorreta");
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
                AddNotification(nameof(redefinicaoSenha), "O objeto redefinição de minha senha não deve ser nulo");
                return;
            }
            AddNotifications(redefinicaoSenha);
            if (Invalid) return;

            Senha = redefinicaoSenha.NovaSenha;
        }
    }
}
