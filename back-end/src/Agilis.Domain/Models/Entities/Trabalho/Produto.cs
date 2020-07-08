using Agilis.Domain.Models.ForeignKeys.Pessoas;
using Agilis.Domain.Models.ForeignKeys.Trabalho;
using DDS.Domain.Core.Abstractions.Model.Entities;
using Flunt.Validations;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Agilis.Domain.Models.Entities.Trabalho
{
    public class Produto : Entity
    {
        public Guid TimeId { get; set; }
        public string Nome { get; private set; }
        public IEnumerable<TemaFK> Temas { get; private set; }
        public IEnumerable<AtorFK> Atores { get; private set; }

        protected Produto()
        {

        }

        public Produto(string nome,
                       Guid timeId,
                       IEnumerable<TemaFK> temas,
                       IEnumerable<AtorFK> atores
            )
        {
            AddNotifications(new Contract()
                .IsNotNullOrEmpty(nome, nameof(Nome), "Nome inválido")
                .IsNotEmpty(timeId, nameof(TimeId), "O id do time não pode ser vazio")
                .IsNotNull(temas, nameof(Temas), "Lista de temas não deve ser nula")
                .IsNotNull(atores, nameof(Atores), "Lista de atores não deve ser nula")
                );

            Nome = nome;
            TimeId = timeId;
            Temas = temas;
            Atores = atores;
        }

        public override string ToString() => Nome;

        internal void AdicionarTema(TemaFK temaFK)
        {
            var novaLista = Temas.ToList();
            novaLista.Add(temaFK);
            Temas = novaLista;
        }

        internal void AdicionarAtor(AtorFK atorFK)
        {
            var novaLista = Atores.ToList();
            novaLista.Add(atorFK);
            Atores = novaLista;
        }
    }
}
