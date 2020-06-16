using Agilis.Domain.Models.ForeignKeys.Trabalho;
using DDS.WebAPI.Abstractions.ViewModels;
using System;
using System.Collections.Generic;

namespace Agilis.WebAPI.ViewModels.Trabalho
{
    public class ReleaseViewModel : IViewModel
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        
        /// <summary>
        /// Sprints da release
        /// </summary>
        public IEnumerable<SprintFK> Sprints { get; set; }
    }
}
