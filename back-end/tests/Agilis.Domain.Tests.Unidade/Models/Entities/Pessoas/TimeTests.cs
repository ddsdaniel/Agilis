using Agilis.Domain.Enums;
using Agilis.Domain.Models.Entities.Pessoas;
using Agilis.Domain.Mocks.Entities.Pessoas;
using Xunit;
using System.Collections.Generic;
using System;
using Agilis.Domain.Mocks.ValueObjects;
using Agilis.Domain.Models.ForeignKeys.Pessoas;
using Agilis.Domain.Models.ForeignKeys.Trabalho;

namespace Agilis.Domain.Tests.Unidade.Models.Entities.Pessoas
{
    public class TimeTests
    {
        [Fact]
        public void Construtor_DadosValidos_Valid()
        {
            //Arrange & Act
            var time = TimeMock.ObterTimePessoalValido();

            //Assert
            Assert.True(time.Valid);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public void Construtor_NomeInvalido_Invalid(string nome)
        {
            //Arrange & Act
            var time = new Time(nome: nome,
                                escopo: EscopoTime.Pessoal,
                                colaboradores: new List<UsuarioFK>(),
                                administradores: new List<UsuarioFK> { new UsuarioFK(Guid.NewGuid(), "Usuário 1", EmailMock.ObterValido().Endereco) },
                                releases: new List<ReleaseFK>(),
                                produtos: new List<ProdutoFK>()
                                );

            //Assert
            Assert.True(time.Invalid);
        }
        
        [Fact]
        public void Construtor_SemAdministrador_Invalid()
        {
            //Arrange & Act
            var time = new Time(nome: "Time 1",
                                escopo: EscopoTime.Pessoal,
                                colaboradores: new List<UsuarioFK>(),
                                administradores: new List<UsuarioFK> { },
                                releases: new List<ReleaseFK>(),
                                produtos: new List<ProdutoFK>()
                                );

            //Assert
            Assert.True(time.Invalid);
        }

    }
}
