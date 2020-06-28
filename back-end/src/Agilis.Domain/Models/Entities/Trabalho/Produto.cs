using Agilis.Domain.Enums;
using Agilis.Domain.Models.ValueObjects.Especificacao;
using Agilis.Domain.Models.ValueObjects.Trabalho;
using DDS.Domain.Core.Abstractions.Model.Entities;
using DDS.Domain.Core.Extensions;
using Flunt.Validations;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Agilis.Domain.Models.Entities.Trabalho
{
    public class Produto : Entity
    {
        public string Nome { get; private set; }
        public IEnumerable<Jornada> Jornadas { get; private set; }
        public IEnumerable<RequisitoNaoFuncional> RequisitosNaoFuncionais { get; private set; }
        public LinguagemUbiqua LinguagemUbiqua { get; private set; }

        protected Produto()
        {

        }

        public Produto(string nome)
            : this(nome,
                  new List<RequisitoNaoFuncional>(),
                  new LinguagemUbiqua(new List<JargaoDoNegocio>()),
                  new List<Jornada>()
                  )
        {

        }

        public Produto(string nome,
                       ICollection<RequisitoNaoFuncional> requisitosNaoFuncionais,
                       LinguagemUbiqua linguagemUbiqua,
                       IEnumerable<Jornada> jornadas)
        {
            AddNotifications(new Contract()
                .IsNotNullOrEmpty(nome, nameof(Nome), "Nome inválido")
                .IsValidArray(requisitosNaoFuncionais, "requisitos não-funcionais")
                .IsNotNull(linguagemUbiqua, nameof(LinguagemUbiqua), "Linguagem Ubíqua não pode ser nula")
                .IfNotNull(linguagemUbiqua, c => c.Join(linguagemUbiqua))
                .IsValidArray(jornadas, nameof(jornadas))
                );

            Nome = nome;
            RequisitosNaoFuncionais = requisitosNaoFuncionais;
            LinguagemUbiqua = linguagemUbiqua;
            Jornadas = jornadas;
        }

        internal Fase AdicionarFaseJornada(Jornada jornada, string nome)
        {
            if (jornada == null)
            {
                AddNotification(nameof(jornada), "Jornada não pode ser nula");
                return null;
            }

            if (jornada.Invalid)
            {
                AddNotifications(jornada);
                return null;
            }

            var posicao = jornada.Fases.MaxOrDefault(f => f.Posicao) + 1;
            var fase = new Fase(posicao, nome);
            jornada.AdicionarFase(fase);            
            if (jornada.Invalid)
            {
                AddNotifications(jornada);
                return null;                
            }

            return fase;
        }

        public void AdicionarRNF(RequisitoNaoFuncional rnf)
        {
            if (rnf == null)
            {
                AddNotification(nameof(rnf), "RNF não pode ser nulo");
                return;
            }

            if (rnf.Invalid)
            {
                AddNotifications(rnf);
                return;
            }

            if (RequisitosNaoFuncionais.Any(r => r.Numero == rnf.Numero))
            {
                AddNotification(nameof(rnf.Numero), $"Já existe um requisito não funcional com o número {rnf.Numero}");
                return;
            }

            var novaLista = RequisitosNaoFuncionais.ToList();
            novaLista.Add(rnf);
            RequisitosNaoFuncionais = novaLista;
        }

        internal void ExcluirJornada(int posicao)
        {
            if (!Jornadas.Any(j => j.Posicao == posicao))
                AddNotification(nameof(posicao), "Jornada não encontrada");
            else
            {
                Jornadas = Jornadas.Where(j => j.Posicao != posicao);
            }
        }

        public void RemoverRNF(int numero)
        {
            if (!RequisitosNaoFuncionais.Any(rnf => rnf.Numero == numero))
                AddNotification(nameof(numero), "RNF não encontrado");
            else
            {
                RequisitosNaoFuncionais = RequisitosNaoFuncionais.Where(rnf => rnf.Numero != numero);
            }
        }

        public void AdicionarJornada(Jornada jornada)
        {
            if (jornada == null)
            {
                AddNotification(nameof(jornada), "Jornada não pode ser nula");
                return;
            }

            if (jornada.Invalid)
            {
                AddNotifications(jornada);
                return;
            }

            if (Jornadas.Any(j => j.Posicao == jornada.Posicao))
            {
                AddNotification(nameof(jornada.Nome), $"Já existe uma jornada com a posição {jornada.Posicao}");
                return;
            }

            var novaLista = Jornadas.ToList();
            novaLista.Add(jornada);
            Jornadas = novaLista.OrderBy(j => j.Posicao);
        }

        public void AtualizarDescricaoRnf(int numeroRnf, string descricao)
        {
            var rnf = RequisitosNaoFuncionais.FirstOrDefault(r => r.Numero == numeroRnf);
            if (rnf == null)
            {
                AddNotification(nameof(numeroRnf), "RNF não encontrado");
                return;
            }

            var indice = RequisitosNaoFuncionais.ToList().FindIndex(r => r.Numero == numeroRnf);

            var rnfAtualizado = new RequisitoNaoFuncional(rnf.Numero, descricao, rnf.Tipo, rnf.Autor);

            if (rnfAtualizado.Invalid)
            {
                AddNotifications(rnfAtualizado);
                return;
            }

            RemoverRNF(rnf.Numero);
            AdicionarRNF(rnfAtualizado);

            RequisitosNaoFuncionais = RequisitosNaoFuncionais
                .OrderBy(r => r.Numero)
                .ToList();
        }

        internal void RenomearJornada(int posicao, string nome)
        {
            var jornada = Jornadas.FirstOrDefault(j => j.Posicao == posicao);
            if (jornada == null)
            {
                AddNotification(nameof(posicao), "Jornada não encontrada");
                return;
            }

            jornada.Renomear(nome);
            if (jornada.Invalid)
            {
                AddNotifications(jornada);
                return;
            }
        }

        public void AtualizarTipoRnf(int numeroRnf, TipoRequisitoNaoFuncional tipo)
        {
            var rnf = RequisitosNaoFuncionais.FirstOrDefault(r => r.Numero == numeroRnf);
            if (rnf == null)
            {
                AddNotification(nameof(numeroRnf), "RNF não encontrado");
                return;
            }

            var indice = RequisitosNaoFuncionais.ToList().FindIndex(r => r.Numero == numeroRnf);

            var rnfAtualizado = new RequisitoNaoFuncional(rnf.Numero, rnf.Descricao, tipo, rnf.Autor);

            if (rnfAtualizado.Invalid)
            {
                AddNotifications(rnf);
                return;
            }

            RemoverRNF(rnf.Numero);
            AdicionarRNF(rnfAtualizado);

            RequisitosNaoFuncionais = RequisitosNaoFuncionais
                .OrderBy(r => r.Numero)
                .ToList();
        }

        public void Renomear(string nome)
        {
            if (String.IsNullOrEmpty(nome))
                AddNotification(nameof(nome), "Nome não deve ser nulo ou vazio");
            else
                Nome = nome;
        }

        public override string ToString() => Nome;
    }
}
