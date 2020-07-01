using DDS.WebAPI.Abstractions.ViewModels;
using System.Collections.Generic;

namespace Agilis.WebAPI.ViewModels.Trabalho
{
    /// <summary>
    /// Representa uma item do product backlog
    /// </summary>
    public class ProductBacklogViewModel : IViewModel
    {
        /// <summary>
        /// Representa as fases de refinamento do produto
        /// </summary>
        public IEnumerable<FaseViewModel> Fases { get;  set; }
    }
}
