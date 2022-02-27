using Bogus;
using Agilis.Core.Domain.Models.ValueObjects;

namespace Agilis.Test.Mock.Domain.Models.ValueObjects
{
    public static class HtmlColorMocks
    {
        public static HtmlColor ObterValido()
        {
            var htmlColor = new Faker<HtmlColor>("pt_BR")
              .CustomInstantiator(faker => new HtmlColor(
                  codigo: faker.Internet.Color()
                  ))
              .Generate();

            return htmlColor;
        }

        public static HtmlColor ObterComCodigo(string codigo)
        {
            return new HtmlColor(codigo);
        }

    }
}
