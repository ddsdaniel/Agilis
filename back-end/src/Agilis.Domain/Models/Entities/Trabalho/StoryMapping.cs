using Agilis.Domain.Models.ValueObjects.Trabalho;
using DDS.Domain.Core.Abstractions.Model.ValueObjects;
using DDS.Domain.Core.Extensions;
using Flunt.Validations;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Agilis.Domain.Models.Entities.Trabalho
{
    public class StoryMapping : ValueObject<StoryMapping>
    {
        public IEnumerable<Tema> Temas { get; private set; }

        public StoryMapping(IEnumerable<Tema> temas)
        {
            AddNotifications(new Contract()
                .IsValidArray(temas, nameof(Temas))
                );

            Temas = temas;
        }

        public void AdicionarTema(Tema tema)
        {
            if (tema == null)
                AddNotification(nameof(tema), "Tema não deve ser nulo");
            else if (tema.Invalid)
                AddNotifications(tema);
            else
            {
                var novaLista = Temas.ToList();
                novaLista.Add(tema);
                Temas = novaLista;
            }
        }

        public void RemoverTema(Guid id)
        {
            if (!Temas.Any(t => t.Id == id))
                AddNotification(nameof(id), "Tema não encontrado");
            else
                Temas = Temas.ToList().Where(t => t.Id != id);
        }
    }
}
