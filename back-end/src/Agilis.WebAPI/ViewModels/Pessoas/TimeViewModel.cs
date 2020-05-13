using Agilis.WebAPI.ViewModels.Pessoas;
using DDS.WebAPI.Abstractions.ViewModels;
using System;

namespace Agilis.WebAPI.ViewModels.Pessoas
{
    /// <summary>
    /// Representa um time, um software
    /// </summary>
    public class TimeViewModel : IViewModel
    {
        /// <summary>
        /// Id do time
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Nome do time
        /// </summary>
        public string Nome { get; set; }

        /// <summary>
        /// Proprietário do time
        /// </summary>
        public Guid UsuarioId { get; set; }

        public bool Favorito { get; set; }
    }
}