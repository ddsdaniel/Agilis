using Agilis.Domain.Models.Entities.Trabalho;
using Agilis.Domain.Models.ForeignKeys;
using Agilis.Domain.Models.ForeignKeys.Trabalho;
using Agilis.Domain.Models.ValueObjects.Trabalho;
using Bogus;
using System.Collections.Generic;

namespace Agilis.Domain.Mocks.Entities.Trabalho
{
    public static class ReleaseMock
    {
        public static Release ObterValido()
            => new Faker<Release>()
               .CustomInstantiator(faker => new Release(faker.System.Version().ToString(),
                                                        sprints: new List<SprintFK>(),
                                                        productBacklog: new ProductBacklog()
                                                        )
               ).Generate();
    }
}
