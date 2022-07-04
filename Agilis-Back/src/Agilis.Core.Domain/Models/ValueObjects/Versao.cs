using Agilis.Core.Domain.Abstractions.Models.ValueObjects;

namespace Agilis.Core.Domain.Models.ValueObjects
{
    public class Versao : ValueObject<Versao>
    {

        public int Major { get; private set; }
        public int Minor { get; private set; }
        public int Revision { get; private set; }

        protected Versao() { }

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
