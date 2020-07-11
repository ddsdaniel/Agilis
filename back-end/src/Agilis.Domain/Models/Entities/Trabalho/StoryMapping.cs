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

        protected StoryMapping()
        {

        }

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
            else if (Temas.Any(t => t.Nome.ToLower().Equals(tema.Nome.ToLower())))
                AddNotification(nameof(tema), "Já existe um tema com este nome");
            else if (Temas.Any(t => t.Id.Equals(tema.Id)))
                AddNotification(nameof(tema), "Já existe um tema com este id");
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

        internal void ExcluirTema(Tema tema)
        {
            if (!Temas.Any(t => t.Id == tema.Id))
                AddNotification(nameof(tema.Id), "Tema não encontrado");
            else
                Temas = Temas.Where(t => t.Id != tema.Id);
        }

        internal void MoverTema(Guid temaId, int novaPosicao)
        {
            if (!Temas.Any(t => t.Id == temaId))
                AddNotification(nameof(temaId), "Tema não encontrado");
            else
            {
                var posicaoAnterior = Temas.ToList().FindIndex(t => t.Id == temaId);
                var novaLista = Temas.ToList();
                Temas = novaLista.Move(posicaoAnterior, novaPosicao);
            }
        }
    }
}
