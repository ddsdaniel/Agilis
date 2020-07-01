using Agilis.Domain.Models.ForeignKeys.Trabalho;
using Agilis.Domain.Models.ValueObjects.Trabalho;
using DDS.Domain.Core.Abstractions.Model.Entities;
using Flunt.Validations;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Agilis.Domain.Models.Entities.Trabalho
{
    public class Release : Entity
    {
        public string Nome { get; private set; }
        public ProductBacklog ProductBacklog { get; private set; }
        public IEnumerable<SprintFK> Sprints { get; private set; }

        protected Release()
        {

        }

        public Release(string nome)
            : this(nome, new List<SprintFK>(), new ProductBacklog())
        {

        }

        public Release(string nome,
                       IEnumerable<SprintFK> sprints,
                       ProductBacklog productBacklog)
        {
            AddNotifications(new Contract()
                .IsNotNullOrEmpty(nome, nameof(Nome), "Nome não pode ser nulo ou vazio")
                .IsNotNull(sprints, nameof(Sprints), "Sprints não deve ser nulo")
                .IsNotNull(productBacklog, nameof(ProductBacklog), "Product Backlog não deve ser nulo")
                .IfNotNull(productBacklog, c => c.Join(productBacklog))
                );

            Nome = nome;
            Sprints = sprints;
            ProductBacklog = productBacklog;
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

        public void Renomear(string nome)
        {
            if (String.IsNullOrEmpty(nome))
                AddNotification(nameof(Nome), "Nome não deve ser nulo ou vazio");
            else
                Nome = nome;
        }
    }
}
