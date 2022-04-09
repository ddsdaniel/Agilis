using Microsoft.Extensions.DependencyInjection;
using Agilis.Application.Services.Notificacoes;
using Agilis.Application.Services.Seguranca;
using Agilis.Infra.Seguranca.Factories;
using Agilis.Application.Services.Times;

namespace Agilis.Infra.CrossCutting.IoC
{
    public static class AppServicesIoC
    {
        public static IServiceCollection AddAppServicesIoC(this IServiceCollection services)
        {
            //Outros
            services.AddScoped<DispositivoCrudAppService>();
            services.AddScoped<TimeCrudAppService>();

            //Segurança
            services.AddScoped<UsuarioCrudAppService>();
            services.AddScoped<AutenticacaoAppService>();
            services.AddScoped<TokenFactory>();
            services.AddScoped<RefreshTokenAppService>();
            services.AddScoped<AlterarMinhaSenhaAppService>();
            services.AddScoped<EsqueciMinhaSenhaAppService>();

            return services;
        }
    }
}
