using DDS.WebAPI.Abstractions.ViewModels;
using System;

namespace Agilis.WebAPI.ViewModels.Trabalho
{
    /// <summary>
    /// Representa um sprint da release
    /// </summary>
    public class SprintViewModel : IViewModel
    {
        /// <summary>
        /// Id do sprint
        /// </summary>
        public Guid Id { get; set; }
        
        /// <summary>
        /// Nome do sprint
        /// </summary>
        public string Nome { get; set; }

        /// <summary>
        /// Intervalo de datas do sprint
        /// </summary>
        public IntervaloDatasViewModel Periodo { get; set; }

    }
}