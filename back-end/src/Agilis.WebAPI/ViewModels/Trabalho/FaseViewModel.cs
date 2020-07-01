using DDS.WebAPI.Abstractions.ViewModels;
using System.Collections.Generic;

namespace Agilis.WebAPI.ViewModels.Trabalho
{
    /// <summary>
    /// Representa a fase de um fluxo
    /// </summary>
    public class FaseViewModel : IViewModel
    {
        /// <summary>
        /// Posição da fase dentro do fluxo
        /// </summary>
        public int Posicao { get; set; }

        /// <summary>
        /// Nome da fase
        /// </summary>
        public string Nome { get; set; }

        /// <summary>
        /// Lista de tarefas desta fase
        /// </summary>
        public IEnumerable<TarefaViewModel> Tarefas { get; set; }
    }
}
