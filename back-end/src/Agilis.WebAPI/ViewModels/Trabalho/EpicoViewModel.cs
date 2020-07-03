using DDS.WebAPI.Abstractions.ViewModels;
using System;

namespace Agilis.WebAPI.ViewModels.Trabalho
{
    /// <summary>
    /// Representa um epico, um software
    /// </summary>
    public class EpicoViewModel : IViewModel
    {
        /// <summary>
        /// Id do epico
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Nome do epico
        /// </summary>
        public string Nome { get; set; }

        /// <summary>
        /// Id do tema ao qual o epico pertence
        /// </summary>
        public Guid TemaId { get; set; }
    }
}