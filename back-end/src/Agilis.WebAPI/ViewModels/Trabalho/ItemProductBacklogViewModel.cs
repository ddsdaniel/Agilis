using Agilis.Domain.Enums;
using DDS.WebAPI.Abstractions.ViewModels;

namespace Agilis.WebAPI.ViewModels.Trabalho
{
    /// <summary>
    /// Representa uma tarefa com a sua respectiva fase e prioridade
    /// </summary>
    public class ItemProductBacklogViewModel : IViewModel
    {
        /// <summary>
        /// Tarefa a ser priorizada
        /// </summary>
        public TarefaViewModel Tarefa { get; private set; }

        /// <summary>
        /// Fase do refinamento em que a tarefa se encontra
        /// </summary>
        public FaseViewModel Fase { get; private set; }

        /// <summary>
        /// Previsão de implementação
        /// </summary>
        public PrioridadeProductBacklog Prioridade { get; private set; }

        /// <summary>
        /// Priorização da tarefa dentro da macro-prioridade e fase
        /// </summary>
        public int Posicao { get; private set; }
    }
}
