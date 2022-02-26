using DDS.Domain.Core.Abstractions.Models.Entities;
using Flunt.Validations;

namespace Agilis.Domain.Abstractions.Entities.Trabalho
{
    public abstract class Tarefa : Entity
    {
        public int Posicao { get; private set; }
        public string Nome { get; private set; }

        //TODO: public int Progresso { get; private set; }
        //TODO: public decimal EsforcoTecnico { get; private set; }
        //TODO: public decimal ValorNegocio { get; private set; }
        //TODO: public IEnumerable<Tag> Tags { get; private set; }
        //TODO: public Nivel Complexidade { get; private set; }
        //TODO: public Nivel Incerteza { get; private set; }

        protected Tarefa()
        {
        }

        protected Tarefa(int posicao, string nome)
        {
            AddNotifications(new Contract()
                .IsGreaterThan(posicao, 0, nameof(Posicao), "Posição deve ser maior que zero")
                .IsNotNullOrEmpty(nome, nameof(Nome), "Nome não deve ser vazio ou nulo")
                );

            Nome = nome;
            Posicao = posicao;
        }

    }
}
