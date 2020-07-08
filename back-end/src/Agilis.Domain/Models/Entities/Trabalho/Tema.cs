using Agilis.Domain.Models.ForeignKeys.Trabalho;
using DDS.Domain.Core.Abstractions.Model.Entities;
using Flunt.Validations;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Agilis.Domain.Models.Entities.Trabalho
{
    public class Tema : Entity
    {
        public Guid ProdutoId { get; set; }
        public string Nome { get; private set; }
        public IEnumerable<EpicoFK> Epicos { get; private set; }

        protected Tema()
        {

        }

        public Tema(string nome, Guid produtoId, IEnumerable<EpicoFK> epicos)
        {
            AddNotifications(new Contract()
                .IsNotNullOrEmpty(nome, nameof(Nome), "Nome inválido")
                .IsNotEmpty(produtoId, nameof(ProdutoId), "O id do produto não pode ser vazio")
                .IsNotNull(epicos, nameof(Epicos), "Lista de épicos não pode ser nula")
                );

            Nome = nome;
            ProdutoId = produtoId;
            Epicos = epicos;
        }

        public override string ToString() => Nome;

        internal void AdicionarEpico(EpicoFK epicoFK)
        {
            var novaLista = Epicos.ToList();
            novaLista.Add(epicoFK);
            Epicos = novaLista;
        }
    }
}
