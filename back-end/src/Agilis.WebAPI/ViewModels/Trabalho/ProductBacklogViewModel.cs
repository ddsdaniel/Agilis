using Agilis.Domain.Enums;
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
        
        /// <summary>
        /// Representa as prioridades em que uma tarefa pode ser classificada
        /// </summary>
        public IEnumerable<PrioridadeProductBacklog> Prioridades { get;  set; }

        /// <summary>
        /// Lista de tarefas com a sua respectiva fase e prioridade
        /// </summary>
        public IEnumerable<ItemProductBacklogViewModel> Itens { get;  set; }
    }
}
