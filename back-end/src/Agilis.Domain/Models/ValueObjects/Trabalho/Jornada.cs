using DDS.Domain.Core.Abstractions.Model.ValueObjects;
using DDS.Domain.Core.Extensions;
using Flunt.Validations;
using System;
using System.Collections.Generic;

namespace Agilis.Domain.Models.ValueObjects.Trabalho
{
    public class Jornada : ValueObject<Jornada>
    {
        public Guid Id { get; private set; }
        public string Nome { get; private set; }
        public IEnumerable<Fase> Fases { get; private set; }

        protected Jornada()
        {
            
        }

        public Jornada(string nome)
            : this(Guid.NewGuid(), nome, new List<Fase>())
        {
            
        }

        public Jornada(Guid id, string nome, IEnumerable<Fase> fases)
        {
            AddNotifications(new Contract()
                .IsNotEmpty(id, nameof(Id), "Id não deve ser vazio")
                .IsNotNullOrEmpty(nome, nameof(Nome), "Nome não deve ser nulo ou vazio")
                .IsValidArray(fases, nameof(fases))
                );

            Id = id;
            Nome = nome;
            Fases = fases;
        }
    }
}
