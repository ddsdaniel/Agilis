using Flunt.Validations;
using Agilis.Core.Domain.Abstractions.Models.ValueObjects;

namespace Agilis.Infra.Seguranca.Models.ValueObjects
{
    public class Senha : ValueObject<Senha>
    {
        public const int TAMANHO_MINIMO = 3;
        public string Conteudo { get; private set; }

        public Senha(string conteudo)
        {
            AddNotifications(new Contract()
                .IsNotNullOrEmpty(conteudo, nameof(Conteudo), "O conteúdo da senha não deve ser vazio ou nulo")
                .HasMinLengthIfNotNullOrEmpty(conteudo, TAMANHO_MINIMO, nameof(TAMANHO_MINIMO), $"O conteúdo da senha deve conter pelo menos {TAMANHO_MINIMO} caracteres")
                );

            Conteudo = conteudo;
        }

        public override string ToString() => Conteudo;
    }
}
