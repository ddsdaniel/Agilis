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
        public Sprint Sprint { get; private set; }
        public SituacaoTarefa Situacao { get; private set; }
        public string Solucao { get; private set; }
        public string Branches { get; private set; }

        public bool AtividadeProgramacao
        {
            get
            {
                var tiposProgramacao = new TipoTarefa[] {
                    TipoTarefa.Melhoria,
                    TipoTarefa.Novidade,
                    TipoTarefa.Bug
                };

                return tiposProgramacao.Contains(Tipo);
            }
        }

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
            IEnumerable<Anexo> anexos,
            Sprint sprint,
            SituacaoTarefa situacao,
            string solucao,
            string branches)
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
            Sprint = sprint;
            Situacao = situacao;
            Solucao = solucao;
            Branches = branches;
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

            if (Situacao == SituacaoTarefa.Feito)
            {
                if (String.IsNullOrEmpty(Solucao))
                    Criticar("Solução inválida para tarefa resolvida");

                if (AtividadeProgramacao && String.IsNullOrEmpty(Branches))
                    Criticar("Branches inválidas para tarefa resolvida");
            }

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
            ImportarCriticas(Sprint);
        }

        public void RemoverAnexo(Guid arquivoid)
        {
            Anexos = Anexos.Where(a => a.ArquivoId != arquivoid);
        }

        public override string ToString() => Titulo;
    }
}
