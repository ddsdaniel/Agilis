using Agilis.Domain.Enums;
using DDS.WebAPI.Abstractions.ViewModels;
using System;

namespace Agilis.WebAPI.ViewModels.Pessoas
{
    /// <summary>
    /// View model usada para cadastrar novos usuários ou para alterar os dados do mesmo
    /// </summary>
    public class UsuarioCadastroViewModel : IViewModel
    {
        /// <summary>
        /// Identificador único do usuário
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// E-mail do usuário, usado para login e recuperação de senha
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Nome do usuário, será concatenado com o sobrenome quando necessário
        /// </summary>
        public string Nome { get; set; }

        /// <summary>
        /// Sobrenome, será concatenado com o nome quando necessário
        /// </summary>
        public string Sobrenome { get; set; }

        /// <summary>
        /// Perfil do usuário, consultar valores possíveis
        /// </summary>
        public RegraUsuario Regra { get; set; }

        /// <summary>
        /// Senha de acesso, não cifrada
        /// </summary>
        public string Senha { get; set; }

        /// <summary>
        /// Validação para conferir se o usuário não errou na digitação da senha
        /// </summary>
        public string ConfirmaSenha { get; set; }
    }
}
