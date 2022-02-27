using Agilis.Infra.Configuracoes.Models.ValueObjects;
using Agilis.Infra.Seguranca.Enums;
using Agilis.Infra.Seguranca.Factories;
using Agilis.Infra.Seguranca.Models.Entities;
using System;

namespace Agilis.Test.Mock.Seguranca.Models.Entities
{
    public static class RefreshTokenMocks
    {
        public static RefreshToken ObterValido()
        {
            var appSettings = new AppSettings
            {
                Segredo = Guid.NewGuid().ToString()
            };
            var tokenFactory = new TokenFactory(appSettings);
            var usuario = UsuarioMocks.ObterValido();
            var token = tokenFactory.Criar(usuario, TipoToken.RefreshToken);
            var refreshToken = new RefreshToken(token);
            return refreshToken;
        }

        public static RefreshToken ObterComToken(string token)
        {
            var refreshToken = new RefreshToken(token);
            return refreshToken;
        }

    }
}
