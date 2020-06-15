using Agilis.Domain.Models.ForeignKeys;
using DDS.Domain.Core.Abstractions.Model.Entities;
using Flunt.Validations;
using System.Collections.Generic;
using System.Linq;

namespace Agilis.Domain.Models.Entities.Trabalho
{
    public class Release : Entity
    {
        public string Nome { get; private set; }
        //TODO: Product Backlog
        public IEnumerable<SprintFK> Sprints { get; private set; }

        protected Release()
        {

        }

        public Release(string nome)
            :this(nome, new List<SprintFK>())
        {

        }

        public Release(string nome,
                       IEnumerable<SprintFK> sprints)
        {
            AddNotifications(new Contract()
                .IsNotNullOrEmpty(nome, nameof(Nome), "Nome não pode ser nulo ou vazio")
                .IsNotNull(sprints, nameof(Sprints), "Sprints não deve ser nulo")
                );

            Nome = nome;
            Sprints = sprints;
        }

        internal void AdicionarSprint(SprintFK sprint)
        {
            if (sprint == null)
            {
                AddNotification(nameof(sprint), "Sprint não deve ser nulo");
                return;
            }

            if (Sprints.Any(s => s.Id == sprint.Id))
            {
                AddNotification(nameof(sprint), "Sprint já adicionado neste produto");
                return;
            }

            var novaLista = Sprints.ToList();
            novaLista.Add(sprint);

            Sprints = novaLista;
        }

        internal void ExcluirSprint(SprintFK sprint)
        {
            if (sprint == null)
            {
                AddNotification(nameof(sprint), "Sprint não deve ser nulo");
                return;
            }

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
