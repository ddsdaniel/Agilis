using DDS.Domain.Core.Abstractions.Model.ValueObjects;
using Flunt.Validations;

namespace Agilis.Domain.Models.ValueObjects.Especificacao
{
    public class JargaoDoNegocio : ValueObject<JargaoDoNegocio>
    {
        public string Jargao { get; private set; }
        public string Significado { get; private set; }
        
        public JargaoDoNegocio(string jargao, string significado)
        {
            AddNotifications(new Contract()
                .IsNotNullOrEmpty(jargao, nameof(Jargao), "Jargão não pode ser nulo")
                .IsNotNullOrEmpty(significado, nameof(Significado), "Significado não pode ser nulo")
                );

            Jargao = jargao;
            Significado = significado;
        }

        public override string ToString() => Jargao;
    }
}
