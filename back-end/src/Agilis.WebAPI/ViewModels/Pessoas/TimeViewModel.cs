using Agilis.Domain.Enums;
using Agilis.WebAPI.ViewModels.Trabalho;
using DDS.WebAPI.Abstractions.ViewModels;
using System;
using System.Collections.Generic;

namespace Agilis.WebAPI.ViewModels.Pessoas
{
    /// <summary>
    /// Representa um time, um software
    /// </summary>
    public class TimeViewModel : IViewModel
    {
        /// <summary>
        /// Id do time
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Nome do time
        /// </summary>
        public string Nome { get; set; }

        /// <summary>
        /// Indica se o time é pessoal ou colaborativo
        /// </summary>
        public EscopoTime Escopo { get; set; }

        public IEnumerable<UsuarioBasicViewModel> Administradores { get; set; }

        public IEnumerable<UsuarioBasicViewModel> Colaboradores { get; set; }

        public IEnumerable<ProdutoBasicViewModel> Produtos { get; set; }        
    }
}