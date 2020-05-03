using Agilis.Domain.Enums;
using DDS.WebAPI.Abstractions.ViewModels;
using System;

namespace Agilis.WebAPI.ViewModels.Seguranca
{
    /// <summary>
    /// Classe usada nos métodos que retornam dados ao front
    /// </summary>
    public class UsuarioConsultaViewModel : IViewModel
    {
        /// <summary>
        /// Id do usuário
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// E-mail do usuário
        /// </summary>
        public string Email { get;  set; }

        /// <summary>
        /// Nome do usuário
        /// </summary>
        public string Nome { get;  set; }

        /// <summary>
        /// Sobrenome do usuário
        /// </summary>
        public string Sobrenome { get;  set; }

        /// <summary>
        /// Indica o perfil do usuário
        /// </summary>
        public RegraUsuario Regra { get; set; }
    }
}
