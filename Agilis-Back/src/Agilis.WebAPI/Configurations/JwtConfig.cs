using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Agilis.Infra.Configuracoes.Abstractions.Models.ValueObjects;
using System;
using System.Text;
using System.Threading.Tasks;

namespace Agilis.WebAPI.Configurations
{
    /// <summary>
    /// Define configurações para do token de autenticação
    /// </summary>
    public static class JwtConfig
    {
        /// <summary>
        /// Adiciona configurações do token
        /// </summary>
        /// <param name="services">Coleção de serviços da startup</param>
        /// <returns>Services configurado para atuar com token jwt</returns>
        public static IServiceCollection AddJwtConfig(this IServiceCollection services)
        {
            var appSettings = services.BuildServiceProvider().GetService<IAppSettings>();
            var key = Encoding.ASCII.GetBytes(appSettings.Segredo);

            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ClockSkew = TimeSpan.Zero
                };

                x.Events = new JwtBearerEvents
                {
                    OnMessageReceived = context =>
                    {
                        var accessToken = context.Request.Query["access_token"];
                        var path = context.HttpContext.Request.Path;
                        if (!string.IsNullOrEmpty(accessToken) && path.StartsWithSegments("/NotificacaoHub"))
                        {
                            context.Token = accessToken;
                        }
                        return Task.CompletedTask;
                    }
                };
            });

            return services;
        }
    }
}
