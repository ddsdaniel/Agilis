using DDS.WebAPI.Abstractions.ViewModels;
using System;
using System.Collections.Generic;

namespace Agilis.WebAPI.ViewModels.Trabalho
{
    /// <summary>
    /// Representa um produto, um software
    /// </summary>
    public class ProdutoViewModel : IViewModel
    {
        /// <summary>
        /// Id do produto
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Nome do produto
        /// </summary>
        public string Nome { get; set; }

        /// <summary>
        /// Jornadas do produto
        /// </summary>
        public IEnumerable<JornadaViewModel> Jornadas { get; set; }
    }
}