using DDS.Domain.Core.Abstractions.Model.ValueObjects;
using DDS.Domain.Core.Extensions;
using Flunt.Validations;
using System;
using System.Collections.Generic;

namespace Agilis.Domain.Models.ValueObjects.Trabalho
{
    public class Jornada : ValueObject<Jornada>
    {
        public int Posicao { get; private set; }
        public string Nome { get; private set; }
        public IEnumerable<Fase> Fases { get; private set; }

        protected Jornada()
        {
            
        }

        public Jornada(int posicao, string nome)
            : this(posicao, nome, new List<Fase>())
        {
            
        }

        public Jornada(int posicao, string nome, IEnumerable<Fase> fases)
        {
            AddNotifications(new Contract()
                .IsGreaterOrEqualsThan(posicao, 0, nameof(Posicao), "Posição não deve ser negativa")
                .IsNotNullOrEmpty(nome, nameof(Nome), "Nome não deve ser nulo ou vazio")
                .IsValidArray(fases, nameof(fases))
                );

            Posicao = posicao;
            Nome = nome;
            Fases = fases;
        }
    }
}
