using System;
using System.Collections.Generic;
using System.Linq;

namespace Agilis.Domain.Abstractions.Entities
{
    public class EntidadeNodo
    {
        public Guid Id { get; private set; }
        public string Nome { get; private set; }
        public IEnumerable<EntidadeNodo> Filhos { get; private set; }

        protected EntidadeNodo()
        {

        }

        public EntidadeNodo(Guid id, string nome)
        {
            Id = id;
            Nome = nome;
            Filhos = new List<EntidadeNodo>();
        }

        public void AdicionarFilho(EntidadeNodo filho)
        {
            var novaLista = Filhos.ToList();
            novaLista.Add(filho);

            Filhos = novaLista;
        }

    }
}
