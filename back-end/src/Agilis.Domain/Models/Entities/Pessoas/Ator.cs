using DDS.Domain.Core.Abstractions.Model.Entities;
using Flunt.Validations;

namespace Agilis.Domain.Models.Entities.Pessoas
{
    /// <summary>
    /// Representa um ator, uma persona do universo Agilis
    /// </summary>
    public class Ator : Entity
    {
        /// <summary>
        /// Nome do ator
        /// </summary>
        public string Nome { get; private set; }

        protected Ator()
        {

        }

        /// <summary>
        /// Construtor completo, com validações
        /// </summary>
        /// <param name="nome">Nome do ator</param>
        public Ator(string nome)
        {
            AddNotifications(new Contract()
                .IsNotNullOrEmpty(nome, nameof(Nome), "NOME_INVALIDO")
                );

            Nome = nome;
        }
    }
}
