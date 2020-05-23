using Agilis.Domain.Abstractions.Services.Trabalho;
using Agilis.Domain.Mocks.Entities.Trabalho;
using Agilis.Domain.Mocks.ValueObjects.Trabalho;
using Agilis.WebAPI.Tests.Integracao.Abstractions;
using Agilis.WebAPI.Tests.Integracao.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Agilis.WebAPI.Tests.Integracao.Tests.Services.Trabalho
{
    public class ProdutoServiceTests : Teste
    {
        public ProdutoServiceTests(CustomWebApplicationFactory<Startup> customWebApplicationFactory) 
            : base(customWebApplicationFactory)
        {
        }

        [Fact]
        public async void AdicionarRnf_DadosValidos_RnfAdicionado()
        {
            //Arrange
            var produtoService = _customWebApplicationFactory.Services.GetService<IProdutoService>();
            var produtoNovo = ProdutoMock.ObterValido();
            await produtoService.Adicionar(produtoNovo);
            var rnf = RequisitoNaoFuncionalMock.ObterValido();

            //Act
            await produtoService.AdicionarRNF(produtoNovo.Id, rnf);

            //Assert
            Assert.True(produtoNovo.Valid);
            Assert.True(rnf.Valid);
            Assert.True(produtoService.Valid);

            var produtoConsultado = await produtoService.ConsultarPorId(produtoNovo.Id);
            Assert.NotNull(produtoConsultado);

            Assert.NotEmpty(produtoConsultado.RequisitosNaoFuncionais);

            var ultimoRNF = produtoConsultado.RequisitosNaoFuncionais.Last();
            Assert.Equal(rnf.Numero, ultimoRNF.Numero);
            Assert.Equal(rnf.Descricao, ultimoRNF.Descricao);
            Assert.Equal(rnf.Autor.Id, ultimoRNF.Autor.Id);
            Assert.Equal(rnf.Tipo, ultimoRNF.Tipo);
        }

        [Fact]
        public async void AdicionarRnf_ProdutoNaoEncontrado_Invalid()
        {
            //Arrange
            var produtoService = _customWebApplicationFactory.Services.GetService<IProdutoService>();
            var produtoNovo = ProdutoMock.ObterValido();
            var contRNF = produtoNovo.RequisitosNaoFuncionais.Count;
            var rnf = RequisitoNaoFuncionalMock.ObterValido();

            //Act
            await produtoService.AdicionarRNF(produtoNovo.Id, rnf);

            //Assert
            Assert.True(produtoNovo.Valid);
            Assert.True(rnf.Valid);
            Assert.True(produtoService.Invalid);
            Assert.Equal(contRNF, produtoNovo.RequisitosNaoFuncionais.Count);
        }

        [Fact]
        public async void AdicionarRnf_RnfInvalido_Invalid()
        {
            //Arrange
            var produtoService = _customWebApplicationFactory.Services.GetService<IProdutoService>();
            var produtoNovo = ProdutoMock.ObterValido();
            var contRNF = produtoNovo.RequisitosNaoFuncionais.Count;
            await produtoService.Adicionar(produtoNovo);
            var rnf = RequisitoNaoFuncionalMock.ObterInvalido();

            //Act
            await produtoService.AdicionarRNF(produtoNovo.Id, rnf);

            //Assert
            Assert.True(produtoNovo.Valid);
            Assert.True(rnf.Invalid);
            Assert.True(produtoService.Invalid);

            var produtoConsultado = await produtoService.ConsultarPorId(produtoNovo.Id);
            Assert.NotNull(produtoConsultado);

            Assert.Equal(contRNF, produtoNovo.RequisitosNaoFuncionais.Count);
            
        }
    }
}
