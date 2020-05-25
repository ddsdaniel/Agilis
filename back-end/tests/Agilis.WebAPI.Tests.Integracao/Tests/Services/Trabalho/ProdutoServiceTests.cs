﻿using Agilis.Domain.Abstractions.Services.Trabalho;
using Agilis.Domain.Enums;
using Agilis.Domain.Mocks.Entities.Trabalho;
using Agilis.Domain.Mocks.ValueObjects.Trabalho;
using Agilis.WebAPI.Tests.Integracao.Abstractions;
using Agilis.WebAPI.Tests.Integracao.Extensions;
using System.Linq;
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
        public async void AdicionarModulo_DadosValidos_RnfAdicionado()
        {
            //Arrange
            var produtoService = _customWebApplicationFactory.Services.GetService<IProdutoService>();
            var produtoNovo = ProdutoMock.ObterValido();
            await produtoService.Adicionar(produtoNovo);
            var modulo = ModuloMock.ObterValido();

            //Act
            await produtoService.AdicionarModulo(produtoNovo.Id, modulo);

            //Assert
            Assert.True(produtoNovo.Valid);
            Assert.True(modulo.Valid);
            Assert.True(produtoService.Valid);

            var produtoConsultado = await produtoService.ConsultarPorId(produtoNovo.Id);
            Assert.NotNull(produtoConsultado);

            Assert.NotEmpty(produtoConsultado.Modulos);

            var ultimoModulo = produtoConsultado.Modulos.Last();
            Assert.Equal(modulo.Numero, ultimoModulo.Numero);
            Assert.Equal(modulo.Nome, ultimoModulo.Nome);
        }

        [Fact]
        public async void AdicionarModulo_ProdutoNaoEncontrado_Invalid()
        {
            //Arrange
            var produtoService = _customWebApplicationFactory.Services.GetService<IProdutoService>();
            var produtoNovo = ProdutoMock.ObterValido();
            var contModulo = produtoNovo.Modulos.Count;
            var modulo = ModuloMock.ObterValido();

            //Act
            await produtoService.AdicionarModulo(produtoNovo.Id, modulo);

            //Assert
            Assert.True(produtoNovo.Valid);
            Assert.True(modulo.Valid);
            Assert.True(produtoService.Invalid);
            Assert.Equal(contModulo, produtoNovo.Modulos.Count);
        }

        [Fact]
        public async void AdicionarModulo_ModuloInvalido_Invalid()
        {
            //Arrange
            var produtoService = _customWebApplicationFactory.Services.GetService<IProdutoService>();
            var produtoNovo = ProdutoMock.ObterValido();
            var contModulo = produtoNovo.Modulos.Count;
            await produtoService.Adicionar(produtoNovo);
            var modulo = ModuloMock.ObterInvalido();

            //Act
            await produtoService.AdicionarModulo(produtoNovo.Id, modulo);

            //Assert
            Assert.True(produtoNovo.Valid);
            Assert.True(modulo.Invalid);
            Assert.True(produtoService.Invalid);

            var produtoConsultado = await produtoService.ConsultarPorId(produtoNovo.Id);
            Assert.NotNull(produtoConsultado);

            Assert.Equal(contModulo, produtoNovo.Modulos.Count);
            
        }

        [Fact]
        public async void RemoverModulo_DadosValidos_ModuloRemovido()
        {
            //Arrange
            var produtoService = _customWebApplicationFactory.Services.GetService<IProdutoService>();
            var produtoNovo = ProdutoMock.ObterValido();
            await produtoService.Adicionar(produtoNovo);
            var modulo = ModuloMock.ObterValido();
            await produtoService.AdicionarModulo(produtoNovo.Id, modulo);

            //Act
            await produtoService.RemoverModulo(produtoNovo.Id, modulo.Numero);

            //Assert
            Assert.True(produtoNovo.Valid);
            Assert.True(modulo.Valid);
            Assert.True(produtoService.Valid);

            var produtoConsultado = await produtoService.ConsultarPorId(produtoNovo.Id);
            Assert.NotNull(produtoConsultado);

            Assert.Null(produtoConsultado.Modulos.FirstOrDefault(r => r.Numero == modulo.Numero));
        }

        [Fact]
        public async void RemoverModulo_ModuloNaoEncontrado_Invalid()
        {
            //Arrange
            var produtoService = _customWebApplicationFactory.Services.GetService<IProdutoService>();
            var produtoNovo = ProdutoMock.ObterValido();
            await produtoService.Adicionar(produtoNovo);
            var moduloNaoEncontrado = produtoNovo.Modulos.Max(r => r.Numero) + 1;
            var contModulo = produtoNovo.Modulos.Count;

            //Act
            await produtoService.RemoverModulo(produtoNovo.Id, moduloNaoEncontrado);

            //Assert
            Assert.True(produtoNovo.Valid);
            Assert.True(produtoService.Invalid);

            var produtoConsultado = await produtoService.ConsultarPorId(produtoNovo.Id);
            Assert.NotNull(produtoConsultado);

            Assert.Equal(contModulo, produtoConsultado.Modulos.Count);
        }

        [Fact]
        public async void RemoverModulo_ProdutoNaoEncontrado_Invalid()
        {
            //Arrange
            var produtoService = _customWebApplicationFactory.Services.GetService<IProdutoService>();
            var produtoNovo = ProdutoMock.ObterValido();

            //Act
            await produtoService.RemoverModulo(produtoNovo.Id, 1);

            //Assert
            Assert.True(produtoNovo.Valid);
            Assert.True(produtoService.Invalid);

            var produtoConsultado = await produtoService.ConsultarPorId(produtoNovo.Id);
            Assert.Null(produtoConsultado);
        }

        [Fact]
        public async void AtualizarNomeModulo_NomeValido_DescricaoAtualizada()
        {
            //Arrange
            var produtoService = _customWebApplicationFactory.Services.GetService<IProdutoService>();
            
            var produtoNovo = ProdutoMock.ObterValido();
            await produtoService.Adicionar(produtoNovo);
            
            var modulo = ModuloMock.ObterValido(1);
            await produtoService.AdicionarModulo(produtoNovo.Id, modulo);

            const string NOVO_NOME = "Novo nome do módulo";

            //Act
            await produtoService.AtualizarNomeModulo(produtoNovo.Id, 1, NOVO_NOME);

            //Assert
            Assert.True(modulo.Valid);
            Assert.True(produtoNovo.Valid);
            Assert.True(produtoService.Valid);

            var produtoConsultado = await produtoService.ConsultarPorId(produtoNovo.Id);
            Assert.NotNull(produtoConsultado);

            Assert.NotEmpty(produtoConsultado.Modulos);

            var moduloAtualizado = produtoConsultado.Modulos.FirstOrDefault(r => r.Numero == modulo.Numero);
            Assert.NotNull(moduloAtualizado);

            Assert.Equal(NOVO_NOME, moduloAtualizado.Nome);
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

        [Fact]
        public async void RemoverRnf_DadosValidos_RnfRemovido()
        {
            //Arrange
            var produtoService = _customWebApplicationFactory.Services.GetService<IProdutoService>();
            var produtoNovo = ProdutoMock.ObterValido();
            await produtoService.Adicionar(produtoNovo);
            var rnf = RequisitoNaoFuncionalMock.ObterValido();
            await produtoService.AdicionarRNF(produtoNovo.Id, rnf);

            //Act
            await produtoService.RemoverRNF(produtoNovo.Id, rnf.Numero);

            //Assert
            Assert.True(produtoNovo.Valid);
            Assert.True(rnf.Valid);
            Assert.True(produtoService.Valid);

            var produtoConsultado = await produtoService.ConsultarPorId(produtoNovo.Id);
            Assert.NotNull(produtoConsultado);

            Assert.Null(produtoConsultado.RequisitosNaoFuncionais.FirstOrDefault(r => r.Numero == rnf.Numero));
        }

        [Fact]
        public async void RemoverRnf_RnfNaoEncontrado_Invalid()
        {
            //Arrange
            var produtoService = _customWebApplicationFactory.Services.GetService<IProdutoService>();
            var produtoNovo = ProdutoMock.ObterValido();
            await produtoService.Adicionar(produtoNovo);
            var rnfNaoEncontrado = produtoNovo.RequisitosNaoFuncionais.Max(r => r.Numero) + 1;
            var contRNF = produtoNovo.RequisitosNaoFuncionais.Count;

            //Act
            await produtoService.RemoverRNF(produtoNovo.Id, rnfNaoEncontrado);

            //Assert
            Assert.True(produtoNovo.Valid);
            Assert.True(produtoService.Invalid);

            var produtoConsultado = await produtoService.ConsultarPorId(produtoNovo.Id);
            Assert.NotNull(produtoConsultado);

            Assert.Equal(contRNF, produtoConsultado.RequisitosNaoFuncionais.Count);
        }

        [Fact]
        public async void RemoverRnf_ProdutoNaoEncontrado_Invalid()
        {
            //Arrange
            var produtoService = _customWebApplicationFactory.Services.GetService<IProdutoService>();
            var produtoNovo = ProdutoMock.ObterValido();

            //Act
            await produtoService.RemoverRNF(produtoNovo.Id, 1);

            //Assert
            Assert.True(produtoNovo.Valid);
            Assert.True(produtoService.Invalid);

            var produtoConsultado = await produtoService.ConsultarPorId(produtoNovo.Id);
            Assert.Null(produtoConsultado);
        }

        [Fact]
        public async void AtualizarDescricaoRnf_DescricaoValida_DescricaoAtualizada()
        {
            //Arrange
            var produtoService = _customWebApplicationFactory.Services.GetService<IProdutoService>();
            
            var produtoNovo = ProdutoMock.ObterValido();
            await produtoService.Adicionar(produtoNovo);
            
            var rnf = RequisitoNaoFuncionalMock.ObterValido(1);
            await produtoService.AdicionarRNF(produtoNovo.Id, rnf);

            const string NOVA_DESCRICAO = "Nova Descrição do RNF";

            //Act
            await produtoService.AtualizarDescricaoRNF(produtoNovo.Id, 1, NOVA_DESCRICAO);

            //Assert
            Assert.True(rnf.Valid);
            Assert.True(produtoNovo.Valid);
            Assert.True(produtoService.Valid);

            var produtoConsultado = await produtoService.ConsultarPorId(produtoNovo.Id);
            Assert.NotNull(produtoConsultado);

            Assert.NotEmpty(produtoConsultado.RequisitosNaoFuncionais);

            var rnfAtualizado = produtoConsultado.RequisitosNaoFuncionais.FirstOrDefault(r => r.Numero == rnf.Numero);
            Assert.NotNull(rnfAtualizado);

            Assert.Equal(NOVA_DESCRICAO, rnfAtualizado.Descricao);
        }

        [Fact]
        public async void AtualizarTipoRnf_TipoValido_TipoAtualizado()
        {
            //Arrange
            var produtoService = _customWebApplicationFactory.Services.GetService<IProdutoService>();

            var produtoNovo = ProdutoMock.ObterValido();
            await produtoService.Adicionar(produtoNovo);

            var rnf = RequisitoNaoFuncionalMock.ObterValido(1);
            await produtoService.AdicionarRNF(produtoNovo.Id, rnf);

            var novoTipo = (TipoRequisitoNaoFuncional)(rnf.Tipo == 0 ? 1 : ((int)rnf.Tipo) - 1);

            //Act
            await produtoService.AtualizarTipoRNF(produtoNovo.Id, 1, novoTipo);

            //Assert
            Assert.True(rnf.Valid);
            Assert.True(produtoNovo.Valid);
            Assert.True(produtoService.Valid);

            var produtoConsultado = await produtoService.ConsultarPorId(produtoNovo.Id);
            Assert.NotNull(produtoConsultado);

            Assert.NotEmpty(produtoConsultado.RequisitosNaoFuncionais);

            var rnfAtualizado = produtoConsultado.RequisitosNaoFuncionais.FirstOrDefault(r => r.Numero == rnf.Numero);
            Assert.NotNull(rnfAtualizado);

            Assert.Equal(novoTipo, rnfAtualizado.Tipo);
        }
    }
}
