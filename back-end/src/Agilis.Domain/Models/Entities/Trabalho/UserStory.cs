using Agilis.Domain.Models.ForeignKeys.Pessoas;
using DDS.Domain.Core.Abstractions.Models.Entities;
using DDS.Domain.Core.Abstractions.Models.Entities;
using DDS.Domain.Core.Extensions;
using Flunt.Validations;
using System.Collections.Generic;

namespace Agilis.Domain.Models.Entities.Trabalho
{
    /// <summary>
    /// Representa uma história do usuário, ou seja, uma funcionalidade que ele deseja
    /// </summary>
    public class UserStory : Entity
    {
        /// <summary>
        /// Nome curto da user story
        /// </summary>
        public string Nome { get; private set; }

        /// <summary>
        /// Persona para qual a história será útil
        /// </summary>
        public AtorFK Ator { get; private set; }

        /// <summary>
        /// O que se deseja
        /// </summary>
        public string Narrativa { get; private set; }

        /// <summary>
        /// Para que serve
        /// </summary>
        public string Objetivo { get; private set; }

        /// <summary>
        /// Descrição no formato padrão de uma user story
        /// </summary>
        public string Historia => $"Eu, enquanto {Ator.Nome}, gostaria {Narrativa} para {Objetivo}";

        public IEnumerable<CriterioAceitacao> CriteriosAceitacao { get; private set; }

        /// <summary>
        /// Construtor usado apenas para a serialização e desserialização
        /// </summary>
        protected UserStory()
        {

        }

        /// <summary>
        /// Construtor completo, com validações
        /// </summary>
        /// <param name="nome">Nome da user story</param>
        /// <param name="ator">Persona para qual a história será útil</param>
        /// <param name="narrativa">O que se deseja</param>
        /// <param name="objetivo">Para que serve</param>
        /// <param name="epicoId">Id do épico ao qual a história pertence</param>
        /// <param name="criteriosAceitacao">Critérios de aceitação da história</param>
        public UserStory(string nome,
                         AtorFK ator,
                         string narrativa,
                         string objetivo,
                         IEnumerable<CriterioAceitacao> criteriosAceitacao)
        {
            AddNotifications(new Contract()
                .IsNotNullOrEmpty(nome, nameof(Nome), "Nome  não deve ser nulo ou vazio")
                .IsNotNull(ator, nameof(Ator), "Ator não deve ser nulo")
                .IsNotNullOrEmpty(narrativa, nameof(Narrativa), "Narrativa não deve ser nula ou vazia")
                .IsNotNullOrEmpty(objetivo, nameof(Objetivo), "Objetivo não deve ser nulo ou vazio")
                .IsValidArray(criteriosAceitacao, nameof(CriteriosAceitacao))
                );

            Nome = nome;
            Ator = ator;
            Narrativa = narrativa;
            Objetivo = objetivo;
            CriteriosAceitacao = criteriosAceitacao;
        }
    }
}
