using Agilis.Application.ViewModels.Produtos;
using Agilis.Core.Domain.Enums;
using System;

namespace Agilis.Application.ViewModels.Tarefas
{
    public class TarefaViewModel
    {
        public Guid Id { get; set; }
        public string Titulo { get; set; }
        public string Descricao { get; set; }
        public Guid ProdutoId { get; set; }
        public ProdutoViewModel Produto { get; set; }
        public TipoTarefa Tipo { get; set; }
    }
}
