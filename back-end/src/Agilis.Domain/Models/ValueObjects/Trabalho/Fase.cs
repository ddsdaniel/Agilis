using Agilis.Domain.Models.ForeignKeys.Trabalho;
using DDS.Domain.Core.Abstractions.Model.ValueObjects;
using Flunt.Validations;
using System.Collections.Generic;

namespace Agilis.Domain.Models.ValueObjects.Trabalho
{
    public class Fase : ValueObject<Fase>
    {
        public int Posicao { get; private set; }
        public string Nome { get; private set; }
        public IEnumerable<TarefaFK> Tarefas { get; private set; }

        protected Fase()
        {

        }

        public Fase(int posicao, string nome)
            :this(posicao, nome, new List<TarefaFK>())
        {

        }

        public Fase(int posicao, string nome, IEnumerable<TarefaFK> tarefas)
        {
            AddNotifications(new Contract()
                .IsGreaterThan(posicao, 0, nameof(Posicao), "Posição deve ser maior que zero")
                .IsNotNullOrEmpty(nome, nameof(Nome), "Nome não deve ser nulo ou vazio")
                .IsNotNull(tarefas, nameof(tarefas), "A lista de tarefas não deve ser nula")
                );

            Posicao = posicao;
            Nome = nome;
            Tarefas = tarefas;
        }
    }
}
