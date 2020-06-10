using Agilis.Domain.Models.Entities.Trabalho;
using Agilis.Domain.Mocks.Entities.Trabalho;
using Xunit;
using System.Collections.Generic;
using Agilis.Domain.Mocks.ValueObjects.Trabalho;
using System.Linq;
using Agilis.Domain.Enums;
using Agilis.Domain.Models.ValueObjects.Especificacao;
using Agilis.Domain.Mocks.ValueObjects.Especificacao;
using Agilis.Domain.Models.ValueObjects.Pessoas;
using System;
using Agilis.Domain.Models.ValueObjects.Trabalho;

namespace Agilis.Domain.Tests.Unidade.Models.Entities.Trabalho
{
    public class ProdutoTests
    {
        [Fact]
        public void Construtor_DadosValidos_Valid()
        {
            //Arrange & Act
            var produto = ProdutoMock.ObterValido();

            //Assert
            Assert.True(produto.Valid);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public void Construtor_NomeInvalido_Invalid(string nome)
        {
            //Arrange & Act
            var produto = new Produto(nome,
                                      new TimeVO(Guid.NewGuid(), "Time 1"),
                                      new List<RequisitoNaoFuncional>(),
                                      LinguagemUbiquaMock.ObterValida(),
                                      new List<SprintVO>()
                                      );

            //Assert
            Assert.True(produto.Invalid);
        }

        [Fact]
        public void Construtor_RnfNulo_Invalid()
        {
            //Arrange & Act
            var produto = new Produto(
                nameof(Produto), 
                null, 
                null, 
                LinguagemUbiquaMock.ObterValida(),
                null
                );

            //Assert
            Assert.True(produto.Invalid);
        }

        [Fact]
        public void Construtor_RnfInvalido_Invalid()
        {
            //Arrange & Act
            var produto = new Produto(nameof(Produto),
                                      new TimeVO(Guid.NewGuid(), "Time 1"),
                                      new List<RequisitoNaoFuncional> { RequisitoNaoFuncionalMock.ObterInvalido() },
                                      LinguagemUbiquaMock.ObterValida(),
                                      new List<SprintVO>()
                                      );

            //Assert
            Assert.True(produto.Invalid);
        }

        [Fact]
        public void AdicionarRNF_RnfValido_Valid()
        {
            //Arrange
            var produto = ProdutoMock.ObterValido();
            var contRNF = produto.RequisitosNaoFuncionais.Count;            
            var rnf1 = RequisitoNaoFuncionalMock.ObterValido(1);
            var rnf2 = RequisitoNaoFuncionalMock.ObterValido(2);

            //Act
            produto.AdicionarRNF(rnf1);
            produto.AdicionarRNF(rnf2);

            //Assert
            Assert.True(rnf1.Valid);
            Assert.True(rnf2.Valid);
            Assert.True(produto.Valid);
            Assert.Equal(contRNF + 2, produto.RequisitosNaoFuncionais.Count);
        }

        [Fact]
        public void AdicionarRNF_RnfNulo_Invalid()
        {
            //Arrange
            var produto = ProdutoMock.ObterValido();
            var contRNF = produto.RequisitosNaoFuncionais.Count;

            //Act
            produto.AdicionarRNF(null);

            //Assert
            Assert.True(produto.Invalid);
            Assert.Equal(contRNF, produto.RequisitosNaoFuncionais.Count);
        }

        [Fact]
        public void AdicionarRNF_RnfInvalido_Invalid()
        {
            //Arrange
            var produto = ProdutoMock.ObterValido();
            var contRNF = produto.RequisitosNaoFuncionais.Count;
            var rnf = RequisitoNaoFuncionalMock.ObterInvalido();

            //Act
            produto.AdicionarRNF(rnf);

            //Assert
            Assert.True(rnf.Invalid);
            Assert.True(produto.Invalid);
            Assert.Equal(contRNF, produto.RequisitosNaoFuncionais.Count);
        }

        [Fact]
        public void AdicionarRNF_NumeroDuplicado_Invalid()
        {
            //Arrange
            var produto = ProdutoMock.ObterValido();
            var contRNF = produto.RequisitosNaoFuncionais.Count;
            var rnfPrimeiro = RequisitoNaoFuncionalMock.ObterValido(1);
            var rnfDuplicado = RequisitoNaoFuncionalMock.ObterValido(1);

            //Act
            produto.AdicionarRNF(rnfPrimeiro);
            produto.AdicionarRNF(rnfDuplicado);

            //Assert
            Assert.True(rnfPrimeiro.Valid);
            Assert.True(rnfDuplicado.Valid);
            Assert.True(produto.Invalid);
            Assert.Equal(contRNF + 1, produto.RequisitosNaoFuncionais.Count);
        }

        [Fact]
        public void RemoverRNF_RnfValido_Valid()
        {
            //Arrange
            var produto = ProdutoMock.ObterValido();
            var rnf1 = RequisitoNaoFuncionalMock.ObterValido(1);
            var rnf2 = RequisitoNaoFuncionalMock.ObterValido(2);
            produto.AdicionarRNF(rnf1);
            produto.AdicionarRNF(rnf2);
            var contRNF = produto.RequisitosNaoFuncionais.Count;

            //Act
            produto.RemoverRNF(1);
            produto.RemoverRNF(2);

            //Assert
            Assert.True(rnf1.Valid);
            Assert.True(rnf2.Valid);
            Assert.True(produto.Valid);
            Assert.Equal(contRNF - 2, produto.RequisitosNaoFuncionais.Count);
        }

        [Fact]
        public void RemoverRNF_RnNaoEncontrado_Valid()
        {
            //Arrange
            var produto = ProdutoMock.ObterValido();
            var rnf1 = RequisitoNaoFuncionalMock.ObterValido(1);
            var rnf2 = RequisitoNaoFuncionalMock.ObterValido(2);
            produto.AdicionarRNF(rnf1);
            produto.AdicionarRNF(rnf2);
            var contRNF = produto.RequisitosNaoFuncionais.Count;

            //Act
            var rnfNaoEncontrado = produto.RequisitosNaoFuncionais.Max(r => r.Numero) + 1;
            produto.RemoverRNF(rnfNaoEncontrado);

            //Assert
            Assert.True(rnf1.Valid);
            Assert.True(rnf2.Valid);
            Assert.True(produto.Invalid);
            Assert.Equal(contRNF, produto.RequisitosNaoFuncionais.Count);
        }

        [Fact]
        public void AtualizarDescricaoRnf_DadosValidos_DescricaoAtualizada()
        {
            //Arrange
            var rnf = RequisitoNaoFuncionalMock.ObterValido(1);

            var produto = ProdutoMock.ObterValido();
            produto.AdicionarRNF(rnf);

            const string NOVA_DESCRICAO = "Nova descrição do RNF";

            //Act
            produto.AtualizarDescricaoRnf(1, NOVA_DESCRICAO);

            //Assert
            Assert.True(rnf.Valid);
            Assert.True(produto.Valid);

            var novoRNF = produto.RequisitosNaoFuncionais.FirstOrDefault(r => r.Numero == rnf.Numero);
            Assert.NotNull(novoRNF);
            Assert.Equal(NOVA_DESCRICAO, novoRNF.Descricao);
        }

        [Fact]
        public void AtualizarDescricaoRnf_DescricaoInvalida_NaoAtualizar()
        {
            //Arrange
            var rnf = RequisitoNaoFuncionalMock.ObterValido(1);
            var descricaoOriginal = rnf.Descricao;

            var produto = ProdutoMock.ObterValido();
            produto.AdicionarRNF(rnf);

            //Act
            produto.AtualizarDescricaoRnf(1, null);

            //Assert
            Assert.True(rnf.Valid);
            Assert.True(produto.Invalid);

            var novoRNF = produto.RequisitosNaoFuncionais.FirstOrDefault(r => r.Numero == rnf.Numero);
            Assert.NotNull(novoRNF);
            Assert.Equal(descricaoOriginal, novoRNF.Descricao);
        }

        [Fact]
        public void AtualizarTipoRnf_DadosValidos_TipoAtualizado()
        {
            //Arrange
            var produto = ProdutoMock.ObterValido();

            var rnfOriginal = RequisitoNaoFuncionalMock.ObterValido(1);
            produto.AdicionarRNF(rnfOriginal);
            var novoTipo = (TipoRequisitoNaoFuncional)(rnfOriginal.Tipo == 0 ? 1 : ((int)rnfOriginal.Tipo) - 1);

            //Act
            produto.AtualizarTipoRnf(1, novoTipo);

            //Assert
            Assert.True(rnfOriginal.Valid);
            Assert.True(produto.Valid);

            var novoRNF = produto.RequisitosNaoFuncionais.FirstOrDefault(r => r.Numero == rnfOriginal.Numero);
            Assert.NotNull(novoRNF);
            Assert.Equal(novoTipo, novoRNF.Tipo);
            Assert.NotEqual(novoTipo, rnfOriginal.Tipo);
        }
    }
}
