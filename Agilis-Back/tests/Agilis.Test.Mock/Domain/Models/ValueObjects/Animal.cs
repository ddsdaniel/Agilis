using System;

namespace Agilis.Test.Mock.Domain.Models.ValueObjects
{
    [Serializable]
    public class Animal
    {
        public string Nome { get; set; }
        public string Raca { get; set; }
        public string Cor { get; set; }
        public int Idade { get; set; }
        public DateTime DataNascimento { get; set; }
        public bool Femea { get; set; }
    }
}
