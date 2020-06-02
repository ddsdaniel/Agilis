using Agilis.Domain.Models.Entities.Pessoas;
using DDS.Domain.Core.Abstractions.Model.Entities;
using Flunt.Validations;

namespace Agilis.Domain.Models.Entities.Trabalho
{
    public class Release : Entity
    {
        public string Nome { get; private set; }
        public Time Time { get; private set; }

        protected Release()
        {

        }

        public Release(string nome, Time time)
        {
            AddNotifications(new Contract()
                .IsNotNullOrEmpty(nome, nameof(Nome), "Nome não pode ser nulo ou vazio")
                .IsNotNull(time, nameof(Time), "Time não pode ser nulo")
                .IfNotNull(time, c => c.Join(time))
                );

            Nome = nome;
            Time = time;
        }     
    }
}
