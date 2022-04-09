using Flunt.Validations;
using Agilis.Core.Domain.Abstractions.Models.Entities;

namespace Agilis.Core.Domain.Models.Entities
{
    public class Time : Entidade
    {
        public string Nome { get; private set; }

        protected Time()  { }

        public Time(string nome)
            
        {
            Nome = nome;

            AddNotifications(new Contract()
                .IsNotNullOrEmpty(Nome, nameof(Nome), "Nome do time não deve ser vazio ou nulo")
                );
        }

        public override string ToString() => Nome;
    }
}
