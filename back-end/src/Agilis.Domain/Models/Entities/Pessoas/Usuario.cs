using Agilis.Domain.Abstractions.Entities.Pessoas;
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
    public class Usuario : Entity, IUsuario
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
                .IsNotNullOrEmpty(nome, nameof(Nome), "Nome do usuário inválido")
                .IsNotNullOrEmpty(sobrenome, nameof(Sobrenome), "Sobrenome inválido")
                .IsNotNull(email, nameof(Email), "E-mail inválido")
                .IfNotNull(email, c => c.Join(email))
                .IsNotNull(senha, nameof(Senha), "Senha inválida")
                .IfNotNull(senha, c => c.Join(senha))
                );

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
                AddNotification(nameof(senhaAtual), "Senha atual está incorreta");
                return;
            }

            if (novaSenha.Conteudo != confirmaNovaSenha.Conteudo)
            {
                AddNotification(nameof(novaSenha), "As senhas não conferem");
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
