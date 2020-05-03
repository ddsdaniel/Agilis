using Agilis.Domain.Enums;
using DDS.Domain.Core.Abstractions.Model.Entities;
using DDS.Domain.Core.Model.ValueObjects;
using DDS.Domain.Core.Model.ValueObjects.Seguranca.Senhas;
using Flunt.Validations;

namespace Agilis.Domain.Models.Entities.Pessoas
{
    public class Usuario : Entity
    {
        public const int TAMANHO_MINIMO_SENHA = 8;

        public Email Email { get; private set; }
        public string Nome { get; private set; }
        public string Sobrenome { get; private set; }
        public SenhaMedia Senha { get; private set; }
        public RegraUsuario Regra { get; private set; }

        protected Usuario()
        {

        }

        public Usuario(Email email, string nome, string sobrenome, SenhaMedia senha, RegraUsuario regra)
        {
            AddNotifications(new Contract()
                .IsNotNullOrEmpty(nome, nameof(Nome), "NOME_INVALIDO")
                .IsNotNullOrEmpty(sobrenome, nameof(Sobrenome), "SOBRENOME_INVALIDO")
                .IsNotNull(email, nameof(Email), "EMAIL_INVALIDO")
                .IsNotNull(senha, nameof(Senha), "SENHA_INVALIDA")
                );

            AddNotifications(email, senha);

            Email = email;
            Nome = nome;
            Sobrenome = sobrenome;
            Senha = senha;
            Regra = regra;
        }

        internal void AlterarSenha(SenhaMedia senhaAtual, SenhaMedia novaSenha, SenhaMedia confirmaNovaSenha)
        {
            if (Senha.Conteudo != senhaAtual.Conteudo)
            {
                AddNotification(nameof(senhaAtual), "SENHA_ATUAL_INCORRETA");
                return;
            }

            if (novaSenha.Conteudo != confirmaNovaSenha.Conteudo)
            {
                AddNotification(nameof(novaSenha), "SENHAS_NAO_CONFEREM");
                return;
            }

            if (novaSenha.Invalid)
            {
                AddNotifications(novaSenha);
                return;
            }

            Senha = novaSenha;
        }
    }
}
