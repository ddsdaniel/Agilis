using Agilis.Domain.Abstractions.Entities.Trabalho;
using System;
using System.Collections.Generic;
using System.Text;

namespace Agilis.Domain.Models.ValueObjects.Trabalho.Tarefas
{
    public class Bug : Tarefa
    {
        protected Bug() : base()
        {

        }

        public Bug(string nome) : base(nome)
        {
        }
    }
}
