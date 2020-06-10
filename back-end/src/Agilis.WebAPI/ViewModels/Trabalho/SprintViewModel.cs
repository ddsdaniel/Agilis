using DDS.WebAPI.Abstractions.ViewModels;
using System;

namespace Agilis.WebAPI.ViewModels.Trabalho
{
    /// <summary>
    /// Representa um sprint do produto
    /// </summary>
    public class SprintViewModel : IViewModel
    {
        /// <summary>
        /// Id do sprint
        /// </summary>
        public Guid Id { get; set; }
        
        /// <summary>
        /// /Número do sprint
        /// </summary>
        public int Numero { get; set; }

        /// <summary>
        /// Nome do sprint
        /// </summary>
        public string Nome { get; set; }

        /// <summary>
        /// Produto ao qual o sprint pertence
        /// </summary>
        public ProdutoBasicViewModel Produto { get; set; }

        /// <summary>
        /// Intervalo de datas do sprint
        /// </summary>
        public IntervaloDatasViewModel Periodo { get; set; }

    }
}