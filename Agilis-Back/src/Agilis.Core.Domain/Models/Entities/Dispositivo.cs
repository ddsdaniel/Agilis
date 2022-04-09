using Flunt.Validations;
using Agilis.Core.Domain.Abstractions.Models.Entities;

namespace Agilis.Core.Domain.Models.Entities
{
    public class Dispositivo : Entidade
    {
        public string Token { get; private set; }

        protected Dispositivo()  { }

        public Dispositivo(string token)
            
        {
            Token = token;

            AddNotifications(new Contract()
                .IsNotNullOrEmpty(Token, nameof(Token), "Token do dispositivo não deve ser vazio ou nulo")
                );
        }

        public override string ToString() => Token;
    }
}
