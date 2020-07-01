using DDS.Domain.Core.Abstractions.Model.ValueObjects;
using DDS.Domain.Core.Extensions;
using Flunt.Validations;
using System;
using System.Collections.Generic;
using System.Linq;

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

        internal void AdicionarFase(Fase fase)
        {
            if (fase == null)
            {
                AddNotification(nameof(fase), "Fase não pode ser nula");
                return;
            }

            if (fase.Invalid)
            {
                AddNotifications(fase);
                return;
            }

            if (Fases.Any(f => f.Posicao == fase.Posicao))
            {
                AddNotification(nameof(fase.Posicao), $"Já existe uma fase na posição {fase.Posicao}");
                return;
            }

            var novaLista = Fases.ToList();
            novaLista.Add(fase);
            Fases = novaLista.OrderBy(j => j.Posicao);
        }

        internal void Renomear(string nome)
        {
            if (String.IsNullOrEmpty(nome))
                AddNotification(nameof(Nome), "Nome não deve ser vazio ou nulo");
            else
                Nome = nome;
        }
    }
}
