using Agilis.Domain.Models.ForeignKeys.Pessoas;
using Agilis.Domain.Models.ForeignKeys.Trabalho;
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
        /// Id do time ao qual o produto pertence
        /// </summary>
        public Guid TimeId { get; set; }

        /// <summary>
        /// Lista de atores do produto
        /// </summary>
        public IEnumerable<AtorFK> Atores { get; set; }

        public StoryMappingViewModel StoryMapping { get; private set; }
    }
}