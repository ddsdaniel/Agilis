using Agilis.Domain.Models.ValueObjects.Pessoas;
using DDS.Domain.Core.Abstractions.Model.Entities;
using Flunt.Validations;

namespace Agilis.Domain.Models.Entities.Trabalho
{
    public class Release : Entity
    {
        public string Nome { get; private set; }
        public TimeVO Time { get; private set; }
        public int Ordem { get; private set; }
        //TODO: Sprints
        //TODO: Product Backlog

        protected Release()
        {

        }

        public Release(int ordem, string nome, TimeVO time)
        {
            AddNotifications(new Contract()
                .IsGreaterOrEqualsThan(ordem, 0, nameof(Ordem), "Ordem não deve ser negativo")
                .IsNotNullOrEmpty(nome, nameof(Nome), "Nome não pode ser nulo ou vazio")
                .IsNotNull(time, nameof(Time), "Time não pode ser nulo")
                .IfNotNull(time, c => c.Join(time))
                );

            Ordem = ordem;
            Nome = nome;
            Time = time;
        }

        public override string ToString() => Nome;
    }
}
