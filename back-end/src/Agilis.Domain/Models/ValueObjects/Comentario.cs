using Agilis.Domain.Models.Entities.Pessoas;
using DDS.Domain.Core.Abstractions.Model.ValueObjects;
using Flunt.Validations;
using System;

namespace Agilis.Domain.Models.ValueObjects
{
    /// <summary>
    /// Representa um comentário dentro do sistema, usado como value object em várias entidades
    /// </summary>
    public class Comentario : ValueObject<Comentario>
    {
        /// <summary>
        /// Texto do comentário
        /// </summary>
        public string Texto { get; private set; }

        /// <summary>
        /// Data de criação do comentário
        /// </summary>
        public DateTime DataCriacao { get; private set; }

        /// <summary>
        /// Autor do comentário
        /// </summary>
        public Usuario Autor { get; private set; }

        /// <summary>
        /// Construtor usado apenas para a serialização e desserialização
        /// </summary>
        protected Comentario()
        {

        }

        /// <summary>
        /// Construtor completo, com validações
        /// </summary>
        /// <param name="texto">Texto do comentário</param>
        /// <param name="dataCriacao">Data de criação do comentário</param>
        /// <param name="autor">Autor do comentário</param>
        public Comentario(string texto, DateTime dataCriacao, Usuario autor)
        {
            AddNotifications(new Contract()
                .IsNotNullOrEmpty(texto, nameof(Texto), "TEXTO_INVALIDO")
                .IsLowerOrEqualsThan(DateTime.Now, dataCriacao, nameof(DataCriacao), "DATA_CRIACAO_INVALIDA")
                .IsNotNull(autor, nameof(Autor), "AUTOR_INVALIDO")
                .IfNotNull(autor, c => c.Join(autor))
                );

            Texto = texto;
            DataCriacao = dataCriacao;
            Autor = autor;
        }
    }
}
