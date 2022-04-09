using Agilis.Core.Domain.Abstractions.Models.ValueObjects;
using System;

namespace Agilis.Infra.Seguranca.Models.ValueObjects
{
    public class Senha : ValueObject<Senha>
    {
        public const int TAMANHO_MINIMO = 3;
        public string Conteudo { get; private set; }

        protected Senha() { }

        public Senha(string conteudo)
        {
            Conteudo = conteudo;
            Validar();
        }

        private void Validar()
        {
            if (String.IsNullOrEmpty(Conteudo))
                Criticar("O conteúdo da senha não deve ser vazio ou nulo");

            if (Conteudo.Length < TAMANHO_MINIMO)
                Criticar($"O conteúdo da senha deve conter pelo menos {TAMANHO_MINIMO} caracteres");
        }

        public override string ToString() => Conteudo;
    }
}
