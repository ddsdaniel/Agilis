using Agilis.WebAPI.Tests.Integracao.Helpers;
using Xunit;

namespace Agilis.WebAPI.Tests.Integracao.Abstractions
{
    [Collection(UniqueContextCollection.NAME)]
    public abstract class Teste
    {
        protected readonly CustomWebApplicationFactory<Startup> _customWebApplicationFactory;

        public Teste(CustomWebApplicationFactory<Startup> customWebApplicationFactory)
        {
            _customWebApplicationFactory = customWebApplicationFactory;
        }
    }
}
