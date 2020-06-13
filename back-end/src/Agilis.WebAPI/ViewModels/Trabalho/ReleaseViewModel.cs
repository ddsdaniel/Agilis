using Agilis.WebAPI.ViewModels.Pessoas;
using DDS.WebAPI.Abstractions.ViewModels;
using System;
using System.Collections.Generic;

namespace Agilis.WebAPI.ViewModels.Trabalho
{
    public class ReleaseViewModel : IViewModel
    {
        public Guid Id { get; set; }
        public int Ordem { get; set; }
        public string Nome { get; set; }
        public TimeViewModel Time { get; set; }
        
        /// <summary>
        /// Sprints da release
        /// </summary>
        public IEnumerable<SprintBasicViewModel> Sprints { get; set; }
    }
}
