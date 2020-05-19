using Xunit;

namespace Agilis.WebAPI.Tests.Integracao.Abstractions
{
    public abstract class Teste: IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        protected readonly CustomWebApplicationFactory<Startup> _customWebApplicationFactory;

        public Teste(CustomWebApplicationFactory<Startup> customWebApplicationFactory)
        {
            _customWebApplicationFactory = customWebApplicationFactory;
        }
    }
}
