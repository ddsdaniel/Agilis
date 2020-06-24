using Agilis.Domain.Abstractions.Entities.Trabalho;
using DDS.Domain.Core.Abstractions.Model.ValueObjects;
using DDS.Domain.Core.Extensions;
using Flunt.Validations;
using System.Collections.Generic;

namespace Agilis.Domain.Models.ValueObjects.Trabalho
{
    public class Fase : ValueObject<Fase>
    {
        public int Posicao { get; private set; }
        public string Nome { get; private set; }
        public IEnumerable<Tarefa> Tarefas { get; private set; }

        protected Fase()
        {

        }

        public Fase(int posicao, string nome, IEnumerable<Tarefa> tarefas)
        {
            AddNotifications(new Contract()
                .IsGreaterThan(posicao, 0, nameof(Posicao), "Posição deve ser maior que zero")
                .IsNotNullOrEmpty(nome, nameof(Nome), "Nome não deve ser nulo ou vazio")
                .IsValidArray(tarefas, nameof(tarefas))
                );

            Posicao = posicao;
            Nome = nome;
            Tarefas = tarefas;
        }
    }
}
