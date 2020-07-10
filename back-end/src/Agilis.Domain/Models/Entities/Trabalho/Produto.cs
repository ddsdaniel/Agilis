using Agilis.Domain.Models.ForeignKeys.Pessoas;
using Agilis.Domain.Models.ValueObjects.Trabalho;
using DDS.Domain.Core.Abstractions.Model.Entities;
using Flunt.Validations;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Agilis.Domain.Models.Entities.Trabalho
{
    public class Produto : Entity
    {
        public Guid TimeId { get; set; }
        public string Nome { get; private set; }
        public IEnumerable<AtorFK> Atores { get; private set; }
        public StoryMapping StoryMapping { get; private set; }

        protected Produto()
        {

        }

        public Produto(string nome,
                       Guid timeId,
                       IEnumerable<AtorFK> atores,
                       StoryMapping storyMapping
            )
        {
            AddNotifications(new Contract()
                .IsNotNullOrEmpty(nome, nameof(Nome), "Nome inválido")
                .IsNotEmpty(timeId, nameof(TimeId), "O id do time não pode ser vazio")
                .IsNotNull(atores, nameof(Atores), "Lista de atores não deve ser nula")
                .IsNotNull(storyMapping, nameof(StoryMapping), "Story Mapping não deve ser nulo")
                .IfNotNull(storyMapping, c => c.Join(storyMapping))
                );

            Nome = nome;
            TimeId = timeId;
            Atores = atores;
            StoryMapping = storyMapping;
        }

        public override string ToString() => Nome;

        internal void AdicionarAtor(AtorFK atorFK)
        {
            var novaLista = Atores.ToList();
            novaLista.Add(atorFK);
            Atores = novaLista;
        }        
    }
}
