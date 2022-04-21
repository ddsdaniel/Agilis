using System;
using System.Collections.Generic;

namespace Agilis.Application.ViewModels.Produtos
{
    public class EpicoViewModel
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public ProdutoViewModel Produto { get; set; }
        public IEnumerable<FeatureViewModel> Features { get; set; }

    }
}
