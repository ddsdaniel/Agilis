using Agilis.Domain.Models.ForeignKeys.Pessoas;
using DDS.WebAPI.Abstractions.ViewModels;
using System;
using System.Collections.Generic;

namespace Agilis.WebAPI.ViewModels.Trabalho
{
    /// <summary>
    /// Representa uma história do usuário, ou seja, uma funcionalidade que ele deseja
    /// </summary>
    public class UserStoryViewModel : IViewModel
    {
        public Guid Id { get; set; }

        /// <summary>
        /// Nome curto da user story
        /// </summary>
        public string Nome { get;  set; }

        /// <summary>
        /// Persona para qual a história será útil
        /// </summary>
        public AtorFK Ator { get;  set; }

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
        /// Id do épico ao qual a user story pertence
        /// </summary>
        public Guid EpicoId { get; set; }

        /// <summary>
        /// Critérios de aceitação da user story
        /// </summary>
        public IEnumerable<CriterioAceitacaoViewModel>  CriteriosAceitacao { get; set; }
    }
}
