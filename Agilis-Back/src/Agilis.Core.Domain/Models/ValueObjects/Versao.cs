using Agilis.Core.Domain.Abstractions.Models.ValueObjects;
using System;
using System.Text.RegularExpressions;

namespace Agilis.Core.Domain.Models.ValueObjects
{
    public class Versao : ValueObject<Versao>
    {
        private const string PATTERN = @"^(\d+\.)?(\d+\.)?(\*|\d+)$";

        public int Major { get; private set; }
        public int Minor { get; private set; }
        public int Revision { get; private set; }

        protected Versao() { }

        public Versao(string versao)
        {
            var regex = new Regex(PATTERN);
            var match = regex.Match(versao);

            if (!match.Success)
                Criticar("Versão inválida");
            else
            {
                var vetor = match.Value.Split('.');
                Major = Convert.ToInt32(vetor[0]);
                Minor = Convert.ToInt32(vetor[1]);
                Revision = Convert.ToInt32(vetor[2]);
                Validar();
            }
        }

        public Versao(int major, int minor, int revision)
        {
            Major = major;
            Minor = minor;
            Revision = revision;

            Validar();
        }        

        private void Validar()
        {
            if (Major < 0) Criticar("Major não deve ser negativo");
            if (Minor < 0) Criticar("Minor não deve ser negativo");
            if (Revision < 0) Criticar("Revision não deve ser negativo");
        }

        public override string ToString() => $"{Major}.{Minor}.{Revision}";
    }
}
