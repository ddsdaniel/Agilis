using DDS.Domain.Core.Abstractions.Model.ValueObjects;
using DDS.Domain.Core.Extensions;
using Flunt.Validations;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Agilis.Domain.Models.ValueObjects.Trabalho
{
    public class Tema : ValueObject<Tema>
    {
        public Guid Id { get; private set; }
        public string Nome { get; private set; }
        public IEnumerable<Epico> Epicos { get; private set; }

        public Tema(Guid id, string nome, IEnumerable<Epico> epicos)
        {
            AddNotifications(new Contract()
                .IsNotEmpty(id, nameof(Id), "Id não deve ser nulo")
                .IsNotNullOrEmpty(nome, nameof(Nome), "Nome não deve ser nulo ou vazio")
                .IsValidArray(epicos, nameof(Epicos))
                );

            Id = id;
            Nome = nome;
            Epicos = epicos;
        }

        public void AdicionarEpico(Epico epico)
        {
            if (epico == null)
                AddNotification(nameof(epico), "Épico não deve ser nulo");
            else if (epico.Invalid)
                AddNotifications(epico);
            else
            {
                var novaLista = Epicos.ToList();
                novaLista.Add(epico);
                Epicos = novaLista;
            }
        }

        public void RemoverEpico(Guid id)
        {
            if (!Epicos.Any(t => t.Id == id))
                AddNotification(nameof(id), "Épico não encontrado");
            else
                Epicos = Epicos.ToList().Where(t => t.Id != id);
        }
    }
}
