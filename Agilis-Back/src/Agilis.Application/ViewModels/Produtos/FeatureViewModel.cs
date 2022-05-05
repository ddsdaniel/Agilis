using System;

namespace Agilis.Application.ViewModels.Produtos
{
    public class FeatureViewModel
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public ProdutoViewModel Produto { get; set; }
    }
}
