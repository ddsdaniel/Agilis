using DDS.WebAPI.Abstractions.ViewModels;
using System;

namespace Agilis.WebAPI.ViewModels.Trabalho
{
    /// <summary>
    /// Representa uma tarefa
    /// </summary>
    public abstract class TarefaViewModel : IViewModel
    {
        /// <summary>
        /// Id da tarefa
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Nome da tarefa
        /// </summary>
        public string Nome { get; set; }
    }
}
