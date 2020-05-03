using Agilis.Domain.Enums;
using DDS.Domain.Core.Abstractions.Model.Entities;
using DDS.Domain.Core.Model.ValueObjects;
using DDS.Domain.Core.Model.ValueObjects.Seguranca.Senhas;
using Flunt.Validations;

namespace Agilis.Domain.Models.Entities.Pessoas
{
    /// <summary>
    /// Usuário do Agilis
    /// </summary>
    public class Usuario : Entity
    {
        /// <summary>
        /// Tamanho mínimo para a senha do usuário
        /// </summary>
        public const int TAMANHO_MINIMO_SENHA = 8;

        /// <summary>
        /// Endereço de e-mail do usuário, usado como chave no login
        /// </summary>
        public Email Email { get; private set; }

        /// <summary>
        /// Primeiro nome do usuário
        /// </summary>
        public string Nome { get; private set; }

        /// <summary>
        /// Sobrenome do uusário
        /// </summary>
        public string Sobrenome { get; private set; }

        /// <summary>
        /// Senha do usuário, descriptografada aqui, mas criptografada explicitamente ao persistir e por HTTPS ao trocar dados com o front
        /// </summary>
        public SenhaMedia Senha { get; private set; }

        /// <summary>
        /// Perfil do usuário
        /// </summary>
        public RegraUsuario Regra { get; private set; }

        /// <summary>
        /// Construtor usado apenas para a serialização e desserialização
        /// </summary>
        protected Usuario()
        {

        }

        /// <summary>
        /// Construtor completo, com validações
        /// </summary>
        /// <param name="email">Endereço de e-mail do usuário, usado como chave no login</param>
        /// <param name="nome">Primeiro nome do usuário</param>
        /// <param name="sobrenome">Sobrenome do uusário</param>
        /// <param name="senha">Senha do usuário, descriptografada aqui, mas criptografada explicitamente ao persistir e por HTTPS ao trocar dados com o front</param>
        /// <param name="regra">Perfil do usuário</param>
        public Usuario(Email email,
                       string nome,
                       string sobrenome,
                       SenhaMedia senha,
                       RegraUsuario regra)
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
