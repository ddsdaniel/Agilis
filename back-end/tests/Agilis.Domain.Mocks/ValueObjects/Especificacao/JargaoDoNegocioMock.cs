using Agilis.Domain.Models.ValueObjects.Especificacao;

namespace Agilis.Domain.Mocks.ValueObjects.Especificacao
{
    public static class JargaoDoNegocioMock
    {
        public static JargaoDoNegocio ObterValido()
        {
            return new JargaoDoNegocio(
               jargao: "Linguagem Ubíqua",
               significado: "Documento que especifica a linguagem do negócio, comum a todos os envolvidos, sejam eles, experts do negócio ou da tecnologia"
               );
        }

        public static JargaoDoNegocio ObterInvalido()
        {
            return new JargaoDoNegocio(null, null);
        }
    }
}
