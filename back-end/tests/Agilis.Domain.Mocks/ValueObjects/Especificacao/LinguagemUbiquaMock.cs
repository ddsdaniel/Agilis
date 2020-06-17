using Agilis.Domain.Models.ValueObjects.Especificacao;
using System.Collections.Generic;

namespace Agilis.Domain.Mocks.ValueObjects.Especificacao
{
    public static class LinguagemUbiquaMock
    {
        public static LinguagemUbiqua ObterValida()
        {
            return new LinguagemUbiqua(new List<JargaoDoNegocio> { JargaoDoNegocioMock.ObterValido() });
        }
    }
}
