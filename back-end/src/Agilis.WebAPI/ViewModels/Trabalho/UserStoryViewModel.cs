using Agilis.WebAPI.ViewModels.Pessoas;
using DDS.WebAPI.Abstractions.ViewModels;
using System.Collections.Generic;

namespace Agilis.WebAPI.ViewModels.Trabalho
{
    /// <summary>
    /// Representa uma história do usuário, ou seja, uma funcionalidade que ele deseja
    /// </summary>
    public class UserStoryViewModel : IViewModel
    {
        /// <summary>
        /// Nome curto da user story
        /// </summary>
        public string Nome { get;  set; }

        /// <summary>
        /// Persona para qual a história será útil
        /// </summary>
        public AtorViewModel Ator { get;  set; }
        public ProdutoViewModel Produto { get;  set; }

        /// <summary>
        /// O que se deseja
        /// </summary>
        public string Narrativa { get;  set; }

        /// <summary>
        /// Para que serve
        /// </summary>
        public string Objetivo { get;  set; }

        /// <summary>
        /// Descrição no formato padrão de uma user story
        /// </summary>
        public string Historia { get; set; }

        /// <summary>
        /// Comentários da user story
        /// </summary>
        public ICollection<ComentarioViewModel> Comentarios { get;  set; }

        /// <summary>
        /// Milestone (opcional) da user story
        /// </summary>
        public MilestoneViewModel Milestone { get; private set; }
    }
}
