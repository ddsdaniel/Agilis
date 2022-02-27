using Flunt.Validations;
using Agilis.Core.Domain.Abstractions.Models.ValueObjects;
using System;
using System.Text.RegularExpressions;

namespace Agilis.Core.Domain.Models.ValueObjects
{
    public class HtmlColor : ValueObject<HtmlColor>
    {
        public string Codigo { get; private set; }

        protected HtmlColor()
        {

        }

        public HtmlColor(string codigo)
        {
            Codigo = codigo;

            AddNotifications(new Contract()
                .IsNotNullOrEmpty(Codigo, nameof(Codigo), "O código da cor não deve ser nulo ou vazio")
                .IsTrue(Validar(Codigo), nameof(Codigo), "Código de cor inválido, deve estar no formato #000000")
                );
        }

        private static bool Validar(string codigo)
        {
            if (String.IsNullOrEmpty(codigo))
                return false;

            const string HEX_PATTERN = "^#([A-Fa-f0-9]{6}|[A-Fa-f0-9]{3})$";
            var regex = new Regex(HEX_PATTERN);
            var match = regex.Match(codigo);
            return match.Success;
        }

        public override string ToString() => Codigo;
    }
}
