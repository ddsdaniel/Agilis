using Agilis.Core.Domain.Abstractions.Models.ValueObjects;
using System;
using System.Text.RegularExpressions;

namespace Agilis.Core.Domain.Models.ValueObjects
{
    public class Email : ValueObject<Email>
    {
        public string Endereco { get; private set; }

        protected Email() { }

        public Email(string endereco)
        {
            Endereco = endereco?.ToLower();
            Validar();
        }

        private void Validar()
        {
            if (String.IsNullOrEmpty(Endereco))
                Criticar("E-mail não deve ser nulo ou vazio");
            else
            {
                var regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
                var match = regex.Match(Endereco);

                if (!match.Success)
                    Criticar("E-mail inválido");
            }

        }

        public override string ToString() => Endereco;
    }
}
