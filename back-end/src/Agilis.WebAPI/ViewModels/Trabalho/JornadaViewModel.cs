using DDS.WebAPI.Abstractions.ViewModels;
using System;
using System.Collections.Generic;

namespace Agilis.WebAPI.ViewModels.Trabalho
{
    /// <summary>
    /// Representa uma jornada do usuário ou de um dado
    /// </summary>
    public class JornadaViewModel : IViewModel
    {
        /// <summary>
        /// Id da jornada
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Nome da jornada
        /// </summary>
        public string Nome { get;  set; }

        /// <summary>
        /// Fases de uma jornada
        /// </summary>
        public IEnumerable<FaseViewModel> Fases { get;  set; }
    }
}
