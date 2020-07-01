using Agilis.Domain.Abstractions.Entities.Trabalho;
using System;
using System.Collections.Generic;
using System.Text;

namespace Agilis.Domain.Models.ValueObjects.Trabalho.Tarefas
{
    public class Feature : Tarefa
    {
        protected Feature() : base()
        {

        }

        public Feature(int posicao, string nome) : base(posicao, nome)
        {
        }
    }
}
