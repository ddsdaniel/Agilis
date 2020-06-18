using DDS.Domain.Core.Abstractions.Model.ValueObjects;
using Flunt.Validations;

namespace Agilis.Domain.Models.ValueObjects.Trabalho
{
    public class Fase : ValueObject<Fase>
    {
        public int Posicao { get; private set; }
        public string Nome { get; private set; }

        public Fase(int posicao, string nome)
        {
            AddNotifications(new Contract()
                .IsGreaterThan(posicao, 0, nameof(Posicao), "Posição deve ser maior que zero")
                .IsNotNullOrEmpty(nome, nameof(Nome), "Nome não deve ser nulo ou vazio")
                );

            Posicao = posicao;
            Nome = nome;
        }
    }
}
