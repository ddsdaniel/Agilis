using Flunt.Validations;
using Agilis.Core.Domain.Abstractions.Models.Entities;
using Agilis.Core.Domain.Models.ValueObjects;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;

namespace Agilis.Infra.Seguranca.Models.Entities
{
    public class RefreshToken : Entidade
    {
        public string Token { get; private set; }

        protected RefreshToken() { }

        public RefreshToken(string token)

        {
            Token = token;

            AddNotifications(new Contract()
                .IsNotNullOrEmpty(token, nameof(Token), "Token não deve ser vazio ou nulo")
                );

            if (!String.IsNullOrEmpty(token) && TestarSeExpirou())
                AddNotification(nameof(token), "Token expirado");
        }

        public JwtSecurityToken Decodificar()
        {
            try
            {
                var handler = new JwtSecurityTokenHandler();
                var jwtSecurityToken = handler.ReadJwtToken(Token);
                return jwtSecurityToken;
            }
            catch (Exception exception)
            {
                AddNotification(nameof(exception), exception.Message);
                return null;
            }
        }

        public Email ObterEmail()
        {
            var jwtSecurityToken = Decodificar();
            var enderecoEmail = jwtSecurityToken.Claims.FirstOrDefault(claim => claim.Type == "unique_name").Value;
            var email = new Email(enderecoEmail);
            return email;
        }

        public bool TestarSeExpirou()
        {
            var jwtSecurityToken = Decodificar();

            if (jwtSecurityToken == null) return true;

            var dataExpiracaoToken = DateTimeOffset
                        .FromUnixTimeSeconds(jwtSecurityToken.Payload.Exp.Value)
                        .LocalDateTime;

            var expirou = dataExpiracaoToken < DateTime.Now;
            return expirou;

        }

        public override string ToString() => Token;
    }
}
