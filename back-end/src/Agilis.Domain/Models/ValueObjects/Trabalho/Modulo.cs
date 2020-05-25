using Agilis.Domain.Models.ValueObjects.Especificacao;
using DDS.Domain.Core.Abstractions.Model.ValueObjects;
using Flunt.Validations;
using System.Collections.Generic;
using System.Linq;

namespace Agilis.Domain.Models.ValueObjects.Trabalho
{
    /// <summary>
    /// Representam os módulos do sistema
    /// </summary>
    public class Modulo : ValueObject<Modulo>
    {
        /// <summary>
        /// Auto-numeração
        /// </summary>
        public int Numero { get; private set; }

        /// <summary>
        /// Nome do módulo
        /// </summary>
        public string Nome { get; private set; }

        /// <summary>
        /// Regras de Negócio do módulo
        /// </summary>
        public ICollection<RegraDeNegocio> RegrasDeNegocio { get; private set; }

        /// <summary>
        /// Requisitos funcionais do módulo
        /// </summary>
        public ICollection<RequisitoFuncional> RequisitosFuncionais { get; private set; }

        protected Modulo()
        {

        }

        public Modulo(int numero,
                      string nome, 
                      ICollection<RegraDeNegocio> regraDeNegocios,
                      ICollection<RequisitoFuncional> requisitoFuncionais)
        {
            AddNotifications(new Contract()
                .IsGreaterThan(numero, 0, nameof(Numero), "Número deve ser maior que zero")
                .IsNotNullOrEmpty(nome, nameof(Nome), "Descrição não deve ser nula ou vazia")
                .IsNotNull(regraDeNegocios, nameof(RegrasDeNegocio), "A lista de regras de negócio não deve ser nula")
                .IsNotNull(requisitoFuncionais, nameof(RequisitosFuncionais), "A lista de requisitos funcionais não deve ser nula")
                );

            if (regraDeNegocios != null)
                regraDeNegocios.ToList().ForEach(rn => AddNotifications(rn));

            if (requisitoFuncionais != null)
                requisitoFuncionais.ToList().ForEach(rf => AddNotifications(rf));

            Numero = numero;
            Nome = nome;
            RegrasDeNegocio = regraDeNegocios;
            RequisitosFuncionais = requisitoFuncionais;
        }

        public override string ToString() => Nome;

    }
}
