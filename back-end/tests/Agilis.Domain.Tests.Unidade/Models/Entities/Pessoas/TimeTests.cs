using Agilis.Domain.Enums;
using Agilis.Domain.Models.Entities.Pessoas;
using Agilis.Domain.Mocks.Entities.Pessoas;
using Xunit;
using Agilis.Domain.Models.ValueObjects.Pessoas;
using Agilis.Domain.Models.ValueObjects.Trabalho;
using System.Collections.Generic;
using System;

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
                                colaboradores: new List<UsuarioVO>(),
                                administradores: new List<UsuarioVO> { new UsuarioVO(Guid.NewGuid(), "Usuário 1") },
                                releases: new List<ReleaseVO>(),
                                produtos: new List<ProdutoVO>()
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
                                colaboradores: new List<UsuarioVO>(),
                                administradores: new List<UsuarioVO> { },
                                releases: new List<ReleaseVO>(),
                                produtos: new List<ProdutoVO>()
                                );

            //Assert
            Assert.True(time.Invalid);
        }

    }
}
