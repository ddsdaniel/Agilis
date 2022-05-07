using Agilis.Core.Domain.Abstractions.Models.ValueObjects;
using System.Text.RegularExpressions;

namespace Agilis.Core.Domain.Models.ValueObjects
{
    public class Url : ValueObject<Url>
    {
        private const string PATTERN = @"https?:\/\/(www\.)?[-a-zA-Z0-9@:%._\+~#=]{1,256}\.[a-zA-Z0-9()]{1,6}\b([-a-zA-Z0-9()@:%_\+.~#?&//=]*)";

        public string Endereco { get; private set; }

        protected Url() { }

        public Url(string endereco)
        {
            Endereco = endereco;
            Validar();
        }

        private void Validar()
        {
            if (string.IsNullOrEmpty(Endereco) || !Regex.Match(Endereco, PATTERN).Success)
                Criticar("Url inválida");
        }

        public override string ToString() => Endereco;
    }
}
