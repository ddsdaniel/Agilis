using System;
using System.Collections.Generic;

namespace Agilis.Application.ViewModels.Produtos
{
    public class ProdutoViewModel
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public string UrlRepositorio { get; set; }
        public IEnumerable<EpicoViewModel> Epicos { get; set; }
    }
}
