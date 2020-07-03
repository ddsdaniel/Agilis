using DDS.WebAPI.Abstractions.ViewModels;
using System;

namespace Agilis.WebAPI.ViewModels.Pessoas
{
    /// <summary>
    /// Representa um ator, um software
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

        /// <summary>
        /// Id do produto ao qual o ator pertence
        /// </summary>
        public Guid ProdutoId { get; set; }
    }
}