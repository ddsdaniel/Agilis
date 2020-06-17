using DDS.WebAPI.Abstractions.ViewModels;
using System;

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
    }
}