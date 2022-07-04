using Microsoft.Extensions.DependencyInjection;
using Agilis.Application.Services.Seguranca;
using Agilis.Infra.Seguranca.Factories;
using Agilis.Application.Services.Times;
using Agilis.Application.Services.Tarefas;
using Agilis.Application.Services.Produtos;
using Agilis.Application.Services.Clientes;
using Agilis.Application.Services.Releases;
using Agilis.Application.Services.Sprints;
using Agilis.Application.Services.Features;
using Agilis.Application.Services.Anexos;

namespace Agilis.Infra.CrossCutting.IoC
{
    public static class AppServicesIoC
    {
        public static IServiceCollection AddAppServicesIoC(this IServiceCollection services)
        {
            //Outros
            services.AddScoped<TimeCrudAppService>();

            //Tarefas
            services.AddScoped<TarefaCrudAppService>();            

            //Produtos
            services.AddScoped<ProdutoCrudAppService>();
            services.AddScoped<FeatureCrudAppService>();

            //Releases
            services.AddScoped<ReleaseCrudAppService>();

            //Sprints
            services.AddScoped<SprintCrudAppService>();

            //Clientes
            services.AddScoped<ClienteCrudAppService>();

            //Anexos
            services.AddScoped<AnexoCrudAppService>();            

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
