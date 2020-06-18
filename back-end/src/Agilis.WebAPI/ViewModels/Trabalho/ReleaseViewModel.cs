using Agilis.Domain.Models.ForeignKeys.Trabalho;
using DDS.WebAPI.Abstractions.ViewModels;
using System;
using System.Collections.Generic;

namespace Agilis.WebAPI.ViewModels.Trabalho
{
    /// <summary>
    /// Representa uma release do time
    /// </summary>
    public class ReleaseViewModel : IViewModel
    {
        /// <summary>
        /// Id da release
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Nome da release
        /// </summary>
        public string Nome { get; set; }
        
        /// <summary>
        /// Sprints da release
        /// </summary>
        public IEnumerable<SprintFK> Sprints { get; set; }

        /// <summary>
        /// Product Backlog da release
        /// </summary>
        public ProductBacklogViewModel ProductBacklog { get; set; }
    }
}
