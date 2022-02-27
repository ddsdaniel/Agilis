using Xunit;

namespace Agilis.Test.Integration.Config
{
    [CollectionDefinition(nameof(IntegrationWebTestFixtureCollection))]
    public class IntegrationWebTestFixtureCollection : ICollectionFixture<IntegrationTestFixture>
    {

    }
}
