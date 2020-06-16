using Agilis.Domain.Enums;
using Agilis.Domain.Models.ValueObjects.Especificacao;
using DDS.Domain.Core.Abstractions.Model.Entities;
using Flunt.Validations;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Agilis.Domain.Models.Entities.Trabalho
{
    public class Produto : Entity
    {
        public string Nome { get; private set; }
        public ICollection<RequisitoNaoFuncional> RequisitosNaoFuncionais { get; private set; }
        public LinguagemUbiqua LinguagemUbiqua { get; private set; }
        
        protected Produto()
        {
            
        }

        public Produto(string nome)
            :this(nome, 
                  new List<RequisitoNaoFuncional>(), 
                  new LinguagemUbiqua(new List<JargaoDoNegocio>())
                  )
        {

        }

        public Produto(string nome,
                       ICollection<RequisitoNaoFuncional> requisitosNaoFuncionais,
                       LinguagemUbiqua linguagemUbiqua)
        {
            AddNotifications(new Contract()
                .IsNotNullOrEmpty(nome, nameof(Nome), "Nome inválido")
                .IsNotNull(requisitosNaoFuncionais, nameof(RequisitosNaoFuncionais), "Lista de RNF não pode ser nula")
                .IfNotNull(requisitosNaoFuncionais, c => c.Join(requisitosNaoFuncionais.ToArray()))
                .IsNotNull(linguagemUbiqua, nameof(LinguagemUbiqua), "Linguagem Ubíqua não pode ser nula")
                .IfNotNull(linguagemUbiqua, c => c.Join(linguagemUbiqua))
                );

            Nome = nome;
            RequisitosNaoFuncionais = requisitosNaoFuncionais;
            LinguagemUbiqua = linguagemUbiqua;
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

            RequisitosNaoFuncionais.Add(rnf);
        }

        public void RemoverRNF(int numero)
        {
            var rnf = RequisitosNaoFuncionais.FirstOrDefault(r => r.Numero == numero);

            if (rnf == null)
            {
                AddNotification(nameof(numero), "RNF não encontrado");
                return;
            }

            RequisitosNaoFuncionais.Remove(rnf);
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

            RequisitosNaoFuncionais.Remove(rnf);
            RequisitosNaoFuncionais.Add(rnfAtualizado);

            RequisitosNaoFuncionais = RequisitosNaoFuncionais
                .OrderBy(r => r.Numero)
                .ToList();
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

            RequisitosNaoFuncionais.Remove(rnf);
            RequisitosNaoFuncionais.Add(rnfAtualizado);

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
