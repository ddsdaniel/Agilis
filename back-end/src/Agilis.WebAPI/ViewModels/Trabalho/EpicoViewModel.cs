using Agilis.Domain.Models.ForeignKeys.Trabalho;
using DDS.WebAPI.Abstractions.ViewModels;
using System;
using System.Collections.Generic;

namespace Agilis.WebAPI.ViewModels.Trabalho
{
    /// <summary>
    /// Representa um epico, um software
    /// </summary>
    public class EpicoViewModel : IViewModel
    {
        /// <summary>
        /// Id do epico
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Nome do epico
        /// </summary>
        public string Nome { get; set; }

        public IEnumerable<UserStoryFK> UserStories { get; set; }
    }
}