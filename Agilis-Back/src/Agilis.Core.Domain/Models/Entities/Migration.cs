using Flunt.Validations;
using Agilis.Core.Domain.Abstractions.Models.Entities;

namespace Agilis.Core.Domain.Models.Entities
{
    public class Migration : Entidade
    {
        public string Nome { get; private set; }

        protected Migration() { }

        public Migration(string nome)
        {
            Nome = nome;

            AddNotifications(new Contract()
                .IsNotNullOrEmpty(Nome, nameof(Nome), "Nome da migration não deve ser vazio ou nulo")
                );
        }
    }
}
