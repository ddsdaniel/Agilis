using Agilis.Domain.Enums;
using Agilis.Domain.Models.Entities.Pessoas;
using DDS.Domain.Core.Abstractions.Model.ValueObjects;
using DDS.Domain.Core.Extensions;
using Flunt.Validations;

namespace Agilis.Domain.Models.ValueObjects.Trabalho
{
    /// <summary>
    /// Representam qualidades, propriedades e/ou restrições do produto
    /// </summary>
    public class RequisitoNaoFuncional : ValueObject<RequisitoNaoFuncional>
    {
        public int Numero { get; private set; }
        public string Codigo => $"RNF{Numero}";
        public string Descricao { get; private set; }
        public TipoRequisitoNaoFuncional Tipo { get; private set; }
        public Usuario Autor { get; private set; }

        public RequisitoNaoFuncional(int numero, string descricao, TipoRequisitoNaoFuncional tipo, Usuario autor)
        {
            AddNotifications(new Contract()
                .IsGreaterThan(numero, 0, nameof(Numero), "Número deve ser maior que zero")
                .IsNotNullOrEmpty(descricao, nameof(Descricao), "Descrição não deve ser nula ou vazia")
                .IsNotNull(autor, nameof(Autor), "Autor não deve ser nulo")
                .IfNotNull(autor, c => c.Join(autor))
                );

            Numero = numero;
            Descricao = descricao;
            Tipo = tipo;
            Autor = autor;
        }

        public override string ToString() => $"{Codigo} - {Tipo.GetDescription()}: {Descricao}";

    }
}
