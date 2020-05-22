using Agilis.Domain.Models.Entities.Pessoas;
using Agilis.Domain.Models.ValueObjects.Trabalho;
using DDS.Domain.Core.Abstractions.Model.Entities;
using Flunt.Validations;
using System.Collections.Generic;
using System.Linq;

namespace Agilis.Domain.Models.Entities.Trabalho
{
    public class Produto : Entity
    {
        public string Nome { get; private set; }
        public Time Time { get; private set; }
        public ICollection<RequisitoNaoFuncional> RequisitosNaoFuncionais { get; private set; }
        public string Modulos { get; set; } // > RF e RN
        public int produckBacklog { get; private set; } // > us > criterios aceitacao
        public int LinguagemUbiqua { get; private set; }

        protected Produto()
        {

        }

        public Produto(string nome, ICollection<RequisitoNaoFuncional> requisitosNaoFuncionais) 
        {
            AddNotifications(new Contract()
                .IsNotNullOrEmpty(nome, nameof(Nome), "Nome inválido")
                .IsNotNull(requisitosNaoFuncionais, nameof(RequisitosNaoFuncionais), "Lista de RNF não pode ser nula")                
                );

            if (requisitosNaoFuncionais != null)
                requisitosNaoFuncionais.ToList().ForEach(rnf => AddNotifications(rnf));

            Nome = nome;
            RequisitosNaoFuncionais = requisitosNaoFuncionais;
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
    }
}
