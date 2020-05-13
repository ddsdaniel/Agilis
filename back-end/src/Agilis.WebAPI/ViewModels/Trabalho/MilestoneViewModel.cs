using DDS.WebAPI.Abstractions.ViewModels;
using System;

namespace Agilis.WebAPI.ViewModels.Trabalho
{
    /// <summary>
    /// Marco histórico, ponto significativo na linha do tempo
    /// </summary>
    public class MilestoneViewModel: IViewModel
    {
        /// <summary>
        /// Id do milestone
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Nome do Milestone
        /// </summary>
        public string Nome { get;  set; }

        /// <summary>
        /// Data do marco histórico
        /// </summary>
        public DateTime Marco { get;  set; }
    }
}