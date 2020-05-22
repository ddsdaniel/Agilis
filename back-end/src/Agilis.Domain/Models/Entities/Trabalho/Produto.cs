using Agilis.Domain.Models.Entities.Pessoas;
using Agilis.Domain.Models.ValueObjects.Trabalho;
using DDS.Domain.Core.Abstractions.Model.Entities;
using Flunt.Validations;
using System;
using System.Collections.Generic;

namespace Agilis.Domain.Models.Entities.Trabalho
{
    public class Produto : Entity
    {
        public string Nome { get; private set; }
        public Time Time { get; private set; }
        public IEnumerable<RequisitoNaoFuncional> RequisitosNaoFuncionais { get; private set; }
        public string Modulos { get; set; } // > RF e RN
        public int produckBacklog { get; private set; } // > us > criterios aceitacao
        public int LinguagemUbiqua { get; private set; }

        protected Produto()
        {

        }

        public Produto(string nome) 
        {
            AddNotifications(new Contract()
                .IsNotNullOrEmpty(nome, nameof(Nome), "Nome inválido")
                );

            Nome = nome;
        }

    }
}
