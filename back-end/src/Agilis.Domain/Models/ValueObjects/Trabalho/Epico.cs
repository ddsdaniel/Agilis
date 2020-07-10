using Agilis.Domain.Models.ForeignKeys.Trabalho;
using DDS.Domain.Core.Abstractions.Model.ValueObjects;
using Flunt.Validations;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Agilis.Domain.Models.ValueObjects.Trabalho
{
    public class Epico : ValueObject<Epico>
    {
        public Guid Id { get; private set; }
        public string Nome { get; private set; }
        public IEnumerable<UserStoryFK> UserStories { get; private set; }

        public Epico(Guid id, string nome, IEnumerable<UserStoryFK> userStories)
        {
            AddNotifications(new Contract()
                .IsNotEmpty(id, nameof(Id), "Id não deve ser nulo")
                .IsNotNullOrEmpty(nome, nameof(Nome), "Nome não deve ser nulo ou vazio")
                .IsNotNull(userStories, nameof(UserStories), "Lista de user stories não deve ser nula")
                );

            Id = id;
            Nome = nome;
            UserStories = userStories;
        }

        public void AdicionarUserStoryFK(UserStoryFK userStory)
        {
            if (userStory == null)
                AddNotification(nameof(userStory), "User story não deve ser nula");
            else
            {
                var novaLista = UserStories.ToList();
                novaLista.Add(userStory);
                UserStories = novaLista;
            }
        }

        public void RemoverUserStory(Guid id)
        {
            if (!UserStories.Any(t => t.Id == id))
                AddNotification(nameof(id), "User story não encontrada");
            else
                UserStories = UserStories.ToList().Where(t => t.Id != id);
        }
    }
}
