using DDS.WebAPI.Abstractions.ViewModels;
using System;

namespace Agilis.WebAPI.ViewModels.Trabalho
{
    /// <summary>
    /// Representa um tema, um software
    /// </summary>
    public class TemaViewModel : IViewModel
    {
        /// <summary>
        /// Id do tema
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Nome do tema
        /// </summary>
        public string Nome { get; set; }

        /// <summary>
        /// Id do produto ao qual o tema pertence
        /// </summary>
        public Guid ProdutoId { get; set; }
    }
}