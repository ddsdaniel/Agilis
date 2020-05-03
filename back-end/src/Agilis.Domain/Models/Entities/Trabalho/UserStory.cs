using Agilis.Domain.Models.Entities.Pessoas;
using Agilis.Domain.Models.ValueObjects;
using DDS.Domain.Core.Abstractions.Model.Entities;
using Flunt.Validations;
using System.Collections.Generic;
using System.Linq;

namespace Agilis.Domain.Models.Entities.Trabalho
{
    /// <summary>
    /// Representa uma história do usuário, ou seja, uma funcionalidade que ele deseja
    /// </summary>
    public class UserStory : Entity
    {
        /// <summary>
        /// Persona para qual a história será útil
        /// </summary>
        public Ator Ator { get; private set; }

        /// <summary>
        /// O que se deseja
        /// </summary>
        public string Narrativa { get; private set; }

        /// <summary>
        /// Para que serve
        /// </summary>
        public string Objetivo { get; private set; }

        /// <summary>
        /// Comentários da user story
        /// </summary>
        public ICollection<Comentario> Comentarios { get; private set; }

        /// <summary>
        /// Milestone (opcional) da user story
        /// </summary>
        public Milestone Milestone { get; private set; }

        /// <summary>
        /// Construtor usado apenas para a serialização e desserialização
        /// </summary>
        protected UserStory()
        {

        }

        /// <summary>
        /// Construtor completo, com validações
        /// </summary>
        /// <param name="ator">Persona para qual a história será útil</param>
        /// <param name="narrativa">O que se deseja</param>
        /// <param name="objetivo">Para que serve</param>
        /// <param name="comentarios">Comentários da user story</param>
        /// <param name="milestone">Milestone (opcional) da user story</param>
        public UserStory(Ator ator,
                         string narrativa,
                         string objetivo,
                         ICollection<Comentario> comentarios,
                         Milestone milestone)
        {
            AddNotifications(new Contract()
                .IsNotNull(ator, nameof(Ator), "ATOR_INVALIDO")
                .IfNotNull(ator, c => c.Join(ator))
                .IsNotNullOrEmpty(narrativa, nameof(Narrativa), "NARRATIVA_INVALIDA")
                .IsNotNullOrEmpty(objetivo, nameof(Objetivo), "OBJETIVO_INVALIDO")
                .IsNotNull(comentarios, nameof(Comentarios), "COMENTARIOS_INVALIDOS")
                .IfNotNull(comentarios, c => c.Join(comentarios.ToArray()))
                .IfNotNull(milestone, c => c.Join(milestone))                
                );

            Ator = ator;
            Narrativa = narrativa;
            Objetivo = objetivo;
            Comentarios = comentarios;
            Milestone = milestone;
        }
    }
}
