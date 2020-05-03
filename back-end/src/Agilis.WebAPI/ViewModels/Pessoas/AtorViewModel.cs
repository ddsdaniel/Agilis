using DDS.WebAPI.Abstractions.ViewModels;
using System;

namespace Agilis.WebAPI.ViewModels.Pessoas
{
    /// <summary>
    /// Representa um ator, uma persona do universo Agilis
    /// </summary>
    public class AtorViewModel : IViewModel
    {
        /// <summary>
        /// Id do ator
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Nome do ator
        /// </summary>
        public string Nome { get; set; }
    }
}