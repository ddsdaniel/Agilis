using Agilis.Domain.Models.ValueObjects.Trabalho;
using DDS.Domain.Core.Abstractions.Model.Entities;
using DDS.Domain.Core.Model.ValueObjects;
using Flunt.Validations;

namespace Agilis.Domain.Models.Entities.Trabalho
{
    public class Sprint : Entity
    {
        public int Numero { get; private set; }
        public IntervaloDatas Periodo { get; private set; }

        public string Nome { get; private set; }
        public ProdutoVO Produto { get; private set; }

        public Sprint(string nome, int numero, IntervaloDatas periodo, ProdutoVO produto)
        {
            AddNotifications(new Contract()
                .IsNotNullOrEmpty(nome, nameof(Nome), "Nome não deve ser vazio ou nulo")
                .IsGreaterOrEqualsThan(numero, 0, nameof(Numero), "Número deve ser maior ou igual a zero")
                .IsNotNull(periodo, nameof(Periodo), "Período não deve ser nulo")
                .IfNotNull(periodo, c => c.Join(periodo))
                .IsNotNull(produto, nameof(Produto), "Produto não deve ser nulo")
                .IfNotNull(produto, c => c.Join(produto))                
                );

            Nome = nome;
            Numero = numero;
            Periodo = periodo;
        }

        public override string ToString() => Nome;
    }
}
