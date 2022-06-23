using Agilis.Application.ViewModels.Produtos;

namespace Agilis.Infra.Importacao.Trello.ViewModels
{
    public class ImportacaoViewModel
    {
        public string BoardId { get; set; }
        public ProdutoViewModel Produto { get; set; }
        public bool LimparDados { get; set; }
    }
}
