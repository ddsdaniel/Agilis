using Microsoft.IdentityModel.Tokens;
using Agilis.Infra.Configuracoes.Abstractions.Models.ValueObjects;
using Agilis.Infra.Seguranca.Enums;
using Agilis.Infra.Seguranca.Models.Entities;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Agilis.Infra.Seguranca.Factories
{
    public class TokenFactory
    {
        public const int DIAS_REFRESH_TOKEN = 7;
        private readonly IAppSettings _appSettings;

        public TokenFactory(IAppSettings appSettings)
        {
            _appSettings = appSettings;
        }

        public string Criar(Usuario usuario, TipoToken tipoToken)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Segredo);
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
