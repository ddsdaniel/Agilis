using Agilis.Domain.Abstractions.Services.Seguranca;
using Agilis.Domain.Abstractions.ValueObjects;
using Agilis.Domain.Models.Entities.Pessoas;
using DDS.Domain.Core.Abstractions.Services;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Agilis.Domain.Services.Seguranca
{
    public class TokenService: Service, ITokenService
    {
        private readonly IAppSettings _appSettings;

        public TokenService(IAppSettings appSettings)
        {
            _appSettings = appSettings;
        }

        /// <summary>
        /// Gera um token baseado no usuário
        /// </summary>
        /// <param name="usuario"></param>
        /// <returns></returns>
        public string Gerar(Usuario usuario)
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
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
