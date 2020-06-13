using Agilis.Domain.Models.ValueObjects.Pessoas;
using Agilis.Domain.Models.ValueObjects.Trabalho;
using DDS.Domain.Core.Abstractions.Model.Entities;
using Flunt.Validations;
using System.Collections.Generic;
using System.Linq;

namespace Agilis.Domain.Models.Entities.Trabalho
{
    public class Release : Entity
    {
        public string Nome { get; private set; }
        public TimeVO Time { get; private set; }
        public int Ordem { get; private set; }
        //TODO: Product Backlog
        public IEnumerable<SprintVO> Sprints { get; private set; }

        protected Release()
        {

        }

        public Release(int ordem,
                       string nome,
                       TimeVO time,
                       IEnumerable<SprintVO> sprints)
        {
            AddNotifications(new Contract()
                .IsGreaterOrEqualsThan(ordem, 0, nameof(Ordem), "Ordem não deve ser negativo")
                .IsNotNullOrEmpty(nome, nameof(Nome), "Nome não pode ser nulo ou vazio")
                .IsNotNull(time, nameof(Time), "Time não pode ser nulo")
                .IfNotNull(time, c => c.Join(time))
                .IsNotNull(sprints, nameof(Sprints), "Sprints não deve ser nulo")
                .IfNotNull(sprints, c => c.Join(sprints.ToArray()))
                );

            Ordem = ordem;
            Nome = nome;
            Time = time;
            Sprints = sprints;
        }

        internal void AdicionarSprint(SprintVO sprint)
        {
            if (Sprints.Any(s => s.Id == sprint.Id || s.Numero == sprint.Numero))
            {
                AddNotification(nameof(sprint), "Sprint já adicionado neste produto");
                return;
            }

            var novaLista = Sprints.ToList();
            novaLista.Add(sprint);

            novaLista = novaLista.OrderBy(s => s.Numero).ToList();

            Sprints = novaLista;
        }

        internal void ExcluirSprint(Sprint sprint)
        {
            if (!Sprints.Any(s => s.Id == sprint.Id))
                AddNotification(nameof(sprint.Id), "Sprint não encontrado");
            else
            {
                Sprints = Sprints.Where(s => s.Id != sprint.Id);
            }
        }

        public override string ToString() => Nome;
    }
}
