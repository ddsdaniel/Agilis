using Xunit;

namespace Agilis.WebAPI.Tests.Integracao.Helpers
{
    [CollectionDefinition(NAME)]
    public class UniqueContextCollection : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        public const string NAME = "Unique Context collection";
    }
}
