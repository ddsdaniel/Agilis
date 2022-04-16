using Microsoft.Extensions.DependencyInjection;
using Agilis.Application.Services.Seguranca;
using Agilis.Infra.Seguranca.Factories;
using Agilis.Application.Services.Times;
using Agilis.Application.Services.Tarefas;
using Agilis.Application.Services.Produtos;
using Agilis.Application.Services.Clientes;
using Agilis.Application.Services.Sprints;

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
            services.AddScoped<EpicoCrudAppService>();

            //Sprints
            services.AddScoped<SprintCrudAppService>();

            //Clientes
            services.AddScoped<ClienteCrudAppService>();

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
