using Agilis.WebAPI.ViewModels.Pessoas;
using DDS.WebAPI.Abstractions.ViewModels;
using System;

namespace Agilis.WebAPI.ViewModels.Trabalho
{
    /// <summary>
    /// Representa um comentário dentro do sistema, usado como value object em várias entidades
    /// </summary>
    public class ComentarioViewModel:IViewModel
    {
        /// <summary>
        /// Id do comentário
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Texto do comentário
        /// </summary>
        public string Texto { get;  set; }

        /// <summary>
        /// Data de criação do comentário
        /// </summary>
        public DateTime DataCriacao { get;  set; }

        /// <summary>
        /// Autor do comentário
        /// </summary>
        public UsuarioConsultaViewModel Autor { get;  set; }
    }
}