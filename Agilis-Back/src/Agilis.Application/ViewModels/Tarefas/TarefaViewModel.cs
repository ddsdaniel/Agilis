using Agilis.Application.ViewModels.Clientes;
using Agilis.Application.ViewModels.Produtos;
using Agilis.Application.ViewModels.Seguranca;
using Agilis.Core.Domain.Enums;
using System;
using System.Collections.Generic;

namespace Agilis.Application.ViewModels.Tarefas
{
    public class TarefaViewModel
    {
        public Guid Id { get; set; }
        public string Titulo { get; set; }
        public string Descricao { get; set; }
        public FeatureViewModel Feature { get; set; }
        public TipoTarefa Tipo { get; set; }
        public UsuarioConsultaViewModel Relator { get; set; }
        public UsuarioConsultaViewModel Solucionador { get; set; }
        public string HorasPrevistas { get; set; }
        public string HorasRealizadas { get; set; }
        public IEnumerable<TagViewModel> Tags { get; set; }
        public IEnumerable<CheckListViewModel> CheckLists { get; set; }
        public ClienteViewModel Cliente { get; set; }
    }
}
