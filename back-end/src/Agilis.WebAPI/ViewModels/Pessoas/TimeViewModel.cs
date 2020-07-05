using Agilis.Domain.Enums;
using Agilis.Domain.Models.ForeignKeys.Pessoas;
using Agilis.Domain.Models.ForeignKeys.Trabalho;
using DDS.WebAPI.Abstractions.ViewModels;
using System;
using System.Collections.Generic;

namespace Agilis.WebAPI.ViewModels.Pessoas
{
    /// <summary>
    /// Representa um time
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

        /// <summary>
        /// Administradores do time
        /// </summary>
        public IEnumerable<UsuarioFK> Administradores { get; set; }

        /// <summary>
        /// Colaboradores do time, exceto administradores
        /// </summary>
        public IEnumerable<UsuarioFK> Colaboradores { get; set; }

        /// <summary>
        /// Lista de produtos do time
        /// </summary>
        public IEnumerable<ProdutoFK> Produtos { get; set; }
    }
}