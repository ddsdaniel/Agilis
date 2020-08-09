using DDS.WebAPI.Abstractions.ViewModels;
using System;
using System.Collections.Generic;

namespace Agilis.WebAPI.ViewModels.Trabalho
{
    /// <summary>
    /// Representa um tema, um software
    /// </summary>
    public class TemaViewModel : IViewModel
    {
        /// <summary>
        /// Id do tema
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Nome do tema
        /// </summary>
        public string Nome { get; set; }

        /// <summary>
        /// Lista de épicos do tema
        /// </summary>
        public IEnumerable<EpicoViewModel> Epicos { get; set; }
    }
}