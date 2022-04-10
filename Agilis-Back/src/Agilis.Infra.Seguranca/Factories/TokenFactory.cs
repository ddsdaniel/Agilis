using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Agilis.Core.Domain.Models.Entities.Seguranca;
using Agilis.Core.Domain.Enums;

namespace Agilis.Infra.Seguranca.Factories
{
    public class TokenFactory
    {
        public const int DIAS_REFRESH_TOKEN = 7;
        private const string CHAVE_SECRETA = "9F5F6734-3E75-4707-AF87-2E3D9F034A22";

        public string Criar(Usuario usuario, TipoToken tipoToken)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(CHAVE_SECRETA);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, usuario.Email.Endereco),
                    new Claim(ClaimTypes.Role, usuario.Regra.ToString())
                }),
                Expires = ObterDuracao(tipoToken),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
                TokenType = tipoToken.ToString()
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        private static DateTime ObterDuracao(TipoToken tipoToken)
        {
            return tipoToken == TipoToken.Autenticacao
                ? DateTime.UtcNow.AddMinutes(30) 
                : DateTime.UtcNow.AddDays(DIAS_REFRESH_TOKEN);
        }
    }
}
