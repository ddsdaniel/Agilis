using Agilis.WebAPI.ViewModels.Pessoas;
using DDS.WebAPI.Abstractions.ViewModels;
using System;

namespace Agilis.WebAPI.ViewModels.Trabalho
{
    /// <summary>
    /// Representa um sprint, um software
    /// </summary>
    public class SprintViewModel : IViewModel
    {
        /// <summary>
        /// Id do sprint
        /// </summary>
        public Guid Id { get; set; }
        public int Numero { get; set; }

        /// <summary>
        /// Nome do sprint
        /// </summary>
        public string Nome { get; set; }

        /// <summary>
        /// Time ao qual o sprint pertence
        /// </summary>
        public ProdutoBasicViewModel Produto { get; set; }

        public IntervaloDatasViewModel Periodo { get; set; }

    }
}