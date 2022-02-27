using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Agilis.Infra.Configuracoes.Abstractions.Models.ValueObjects;
using Agilis.Infra.Configuracoes.Models.ValueObjects;

namespace Agilis.Infra.CrossCutting.IoC
{
    public static class ConfiguracoesIoC
    {
        public static IServiceCollection AddConfiguracoesIoC(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<AppSettings>(configuration.GetSection(nameof(AppSettings)));
            services.AddSingleton<IAppSettings>(serviceProvider => serviceProvider.GetRequiredService<IOptions<AppSettings>>().Value);
            return services;
        }
    }
}
