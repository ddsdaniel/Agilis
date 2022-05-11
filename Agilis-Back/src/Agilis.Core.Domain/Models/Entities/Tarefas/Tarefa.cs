using Agilis.Core.Domain.Abstractions.Models.Entities;
using Agilis.Core.Domain.Enums;
using Agilis.Core.Domain.Models.Entities.Seguranca;
using Agilis.Core.Domain.Models.ValueObjects;
using Agilis.Core.Domain.Models.ValueObjects.Tarefas;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Agilis.Core.Domain.Models.Entities.Tarefas
{
    public class Tarefa : Entidade
    {
        public string Titulo { get; private set; }
        public string Descricao { get; private set; }
        public Feature Feature { get; private set; }
        public TipoTarefa Tipo { get; private set; }
        public Usuario Relator { get; private set; }
        public Usuario Solucionador { get; private set; }
        public Hora HorasPrevistas { get; private set; }
        public Hora HorasRealizadas { get; private set; }
        public IEnumerable<Tag> Tags { get; private set; }
        public IEnumerable<CheckList> CheckLists { get; private set; }
        public Cliente Cliente { get; private set; }
        public int Valor { get; private set; }
        public Url UrlTicketSAC { get; private set; }
        public IEnumerable<Comentario> Comentarios { get; private set; }
        public IEnumerable<Anexo> Anexos { get; private set; }        

        protected Tarefa() { }

        public Tarefa(
            string titulo,
            string descricao,
            Feature feature,
            TipoTarefa tipo,
            Usuario relator,
            Usuario solucionador,
            Hora horasPrevistas,
            Hora horasRealizadas,
            IEnumerable<Tag> tags,
            IEnumerable<CheckList> checkLists,
            Cliente cliente,
            int valor,
            Url urlTicketSAC,
            IEnumerable<Comentario> comentarios, 
            IEnumerable<Anexo> anexos)
        {
            Titulo = titulo;
            Descricao = descricao;
            Feature = feature;
            Tipo = tipo;
            Relator = relator;
            Solucionador = solucionador;
            HorasPrevistas = horasPrevistas;
            HorasRealizadas = horasRealizadas;
            Tags = tags;
            CheckLists = checkLists;
            Cliente = cliente;
            Valor = valor;
            UrlTicketSAC = urlTicketSAC;
            Comentarios = comentarios;
            Anexos = anexos;
            Validar();
        }

        private void Validar()
        {
            if (string.IsNullOrEmpty(Titulo))
                Criticar("Título inválido.");

            if (Descricao == null)
                Criticar("Descrição inválida.");

            if (Feature == null)
                Criticar("Feature não deve ser nula");

            if (Relator == null)
                Criticar("Relator não deve ser nulo");

            if (Valor < 0 || Valor > 5)
                Criticar("Valor deve estar entre 0 e 5");

            ImportarCriticas(Feature);
            ImportarCriticas(HorasPrevistas);
            ImportarCriticas(HorasRealizadas);
            ImportarCriticas(Relator);
            ImportarCriticas(Solucionador);
            ImportarCriticas(Tags);
            ImportarCriticas(CheckLists);
            ImportarCriticas(Cliente);
            ImportarCriticas(UrlTicketSAC);
            ImportarCriticas(Comentarios);
            ImportarCriticas(Anexos);
        }

        public void RemoverAnexo(Guid arquivoid)
        {
            Anexos = Anexos.Where(a => a.ArquivoId != arquivoid);
        }

        public override string ToString() => Titulo;
    }
}
