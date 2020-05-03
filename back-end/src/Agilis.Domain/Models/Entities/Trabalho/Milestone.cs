using DDS.Domain.Core.Abstractions.Model.Entities;
using Flunt.Validations;
using System;

namespace Agilis.Domain.Models.Entities.Trabalho
{
    /// <summary>
    /// Marco histórico, ponto significativo na linha do tempo
    /// </summary>
    public class Milestone : Entity
    {
        /// <summary>
        /// Nome do Milestone
        /// </summary>
        public string Nome { get; private set; }

        /// <summary>
        /// Data do marco histórico
        /// </summary>
        public DateTime Marco { get; private set; }

        /// <summary>
        /// Construtor usado apenas para a serialização e desserialização
        /// </summary>
        protected Milestone()
        {

        }

        /// <summary>
        /// Construtor completo, com validações
        /// </summary>
        /// <param name="nome">Nome do Milestone</param>
        /// <param name="marco">Data do marco histórico</param>
        public Milestone(string nome, DateTime marco)
        {
            AddNotifications(new Contract()
                .IsNotNullOrEmpty(nome, nameof(Nome), "NOME_INVALIDO")
                );

            Nome = nome;
            Marco = marco;
        }
    }
}
